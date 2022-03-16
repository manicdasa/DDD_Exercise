import { JsonHubProtocol, HubConnectionState, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { ActionCreators } from '../../store/CustomerReducer';
import { ActionCreatorsForNotifications } from '../../store/NotificationReducer';
import { ActionCreatorsForSidePanel } from '../../store/SidePanelReducer';
import { ActionCreatorsForUserOffers } from '../../store/AuthorOffersReducer';

  const isDev = process.env.NODE_ENV === 'development';
  const connectionHub = '/hubs/chat';
  const notificationHub = '/hubs/notification'
  const options = {
    logMessageContent: isDev,
    logger: isDev ? LogLevel.Warning : LogLevel.Error,
    accessTokenFactory: () => 
    {
        return `Bearer ${localStorage.getItem('token')}`;
    }
  };

  const connection = new HubConnectionBuilder().withUrl(connectionHub, options).withAutomaticReconnect().withHubProtocol(new JsonHubProtocol())
    .configureLogging(LogLevel.Information).build();
    
  const notificationConnection = new HubConnectionBuilder().withUrl(notificationHub, options).withAutomaticReconnect().withHubProtocol(new JsonHubProtocol())
    .configureLogging(LogLevel.Information).build();

  let awaitedConnection : any = null;
  const startSignalRConnection = async (connection:any, notificationConnection: any) => {
    try 
    {
      awaitedConnection = connection.start();
      await notificationConnection.start();
      await awaitedConnection;
    } 
    catch (err) 
    {
        setTimeout(() => startSignalRConnection(connection, notificationConnection), 5000);
    }
  };
  
  export const invokeConnection = async (id: any) => 
  { 
    if(connection.state === HubConnectionState.Connected)
    {
      await connection.invoke('Connect', parseInt(id));
    }
    else
    {
      awaitedConnection.then(async () => 
                          {  
                            await connection.invoke('Connect', parseInt(id));
                          })
    }
  }

  export const stopConnection = async (dispatch: any) =>
  {

    dispatch(ActionCreatorsForNotifications.getNotifications());
    await connection.stop().then(()=>console.log('Connection closed. State of connection is ' + connection.state));
    connection.off('ReceiveMessage');
    await notificationConnection.stop().then(()=>console.log('Notifications connection closed. State of notification connection is ' + notificationConnection.state));
    notificationConnection.off('receiveNotification');    
    notificationConnection.off('refreshSidePanel');
  }
  
  export const setupSignalRConnection = (getAccessToken:any) => (dispatch:any, getState:any) => 
  {
    connection.serverTimeoutInMilliseconds = 60000;
  
    connection.onclose(error => { console.log('Connection closed.')});
    notificationConnection.onclose(error => { console.log('Notification connection closed.')});
    
  
    startSignalRConnection(connection, notificationConnection);

    connection.on('ReceiveMessage', (message: any) => 
    {
        dispatch(ActionCreators.receiveMessage({ ...message, myMessage: (localStorage.getItem('user') === message.username) }));
    });

    notificationConnection.on('receiveNotification', (notification: any) => 
    {
        dispatch(ActionCreatorsForNotifications.receiveNotifications(notification));
    });

    notificationConnection.on('refreshSidePanel', (panelObject: any) => 
    {
      if(panelObject.panelTab === 0)  
      {
          var newPanelObject = {...panelObject.panelObject, isNewReceiveOffer: true }
          if(panelObject.eventType === 0)
          {
              dispatch(ActionCreatorsForSidePanel.receiveAuthorOffer(panelObject.panelObject));
          }
          else if(panelObject.eventType === 1)
          {
              dispatch(ActionCreatorsForSidePanel.removeOffer(panelObject.panelObject));
          }
          else if(panelObject.eventType === 2)
          {
              dispatch(ActionCreatorsForSidePanel.removeOffer(panelObject.panelObject));
              dispatch(ActionCreatorsForSidePanel.modifyAuthorsOffers(newPanelObject));
          }
      }
      else if(panelObject.panelTab === 1)
      {
          var newPanelObject = {...panelObject.panelObject, isNewReceiveBid: true }
          if(panelObject.eventType === 0)
          {
              dispatch(ActionCreatorsForSidePanel.receiveAuthorsMyBids(panelObject.panelObject))
          }
          else if(panelObject.eventType === 1)
          {
              dispatch(ActionCreatorsForSidePanel.removeBid(panelObject.panelObject));
          }
          else if(panelObject.eventType === 2)
          {
              dispatch(ActionCreatorsForSidePanel.removeBid(panelObject.panelObject));
              dispatch(ActionCreatorsForSidePanel.modifyAuthorsBids(newPanelObject));
          }
      }
      else if(panelObject.panelTab === 2)
      {
          if(panelObject.eventType === 0)
          {
              dispatch(ActionCreatorsForUserOffers.receiveAuthorsChatObject(panelObject.panelObject));
              dispatch(ActionCreators.receiveChatObject(panelObject.panelObject));
          }
          else if(panelObject.eventType === 1)
          {
              dispatch(ActionCreatorsForUserOffers.removeChatObject(panelObject.panelObject));
              dispatch(ActionCreators.removeCustomerChatObject(panelObject.panelObject));
          }
          else if(panelObject.eventType === 2)
          {
              dispatch(ActionCreatorsForUserOffers.removeChatObject(panelObject.panelObject));
              dispatch(ActionCreatorsForUserOffers.modifyChatObject(panelObject.panelObject));

              dispatch(ActionCreators.removeCustomerChatObject(panelObject.panelObject));
              dispatch(ActionCreators.modifyCustomerChatObject(panelObject.panelObject));
          }
      }
    });

    return connection;
  };