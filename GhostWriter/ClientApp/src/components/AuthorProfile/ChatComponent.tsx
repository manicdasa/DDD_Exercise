import React, { useEffect, useRef, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Badge, Button, Form, Input, NavLink, PopoverBody, PopoverHeader, UncontrolledPopover } from 'reactstrap';
import { MessageBox } from 'react-chat-elements';
import ScrollableFeed from 'react-scrollable-feed'
import { MdClose } from 'react-icons/md';
import { invokeConnection } from '../Helpers/SignalRMiddleware';
import { GetAllMessagesForProfileChat, SendMessage } from '../../services/ProfileServices';
import { Link } from 'react-router-dom';
import { RiUserVoiceLine, RiMessage3Line } from 'react-icons/ri';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';
import { useAlert } from 'react-alert';

export const ChatComponent = ({ openChats, value } : any) => 
{   
    const dispatch = useDispatch();

    //filtering messages for profile page
    const messages = useSelector((state: any) => state.authorActiveProjectsReducer.messages.filter((x: any) => x.headProposalId === value.headProposalId));

    //message send text
    const [messageText, setmessageText] = useState('');

    const handleCloseChat = (headProposalId: number) => 
    {
        dispatch(ActionCreatorsForChatComponent.closeOpenChat(headProposalId));
    }

    //ref for chat
    const chatContainer: any = React.createRef();
    const messagesEndRef = useRef<HTMLInputElement>(null);
    const chatInputRef = useRef<any>(null);

    const handleMessageInput = (event: any) => 
    {
        setmessageText(event.target.value);
    }

    const alert = useAlert();

    useEffect(() =>
    {
        GetAllMessagesForProfileChat(dispatch, value.headProposalId, alert);
        invokeConnection(value.headProposalId);
    },[]);

    return(
        <div>
            <Button id={`PopoverClick` + value.headProposalId} type="button" className="messages-button chat-button-bottom" style={{ border: '1px solid rgb(93, 169, 255)' }}>
                <div className="chat-bar-text">< RiUserVoiceLine className="icon-cht-button" />{value.customerUsername}</div>
            </Button>
            <UncontrolledPopover id={"chat-popover"+value.headProposalId} trigger="click" placement="top" target={`PopoverClick`+value.headProposalId}>
                <PopoverHeader>
                    <div className="row-style">
                        <div className="title-chat"><div className="chat-icon-cont"><RiMessage3Line className="icon-cht-header" /></div> <div className="chat-username-top"> {value.customerUsername}</div> </div>
                        <div><MdClose color="#4E74DE" className="my-cursor-style close-icon-chat" onClick={() => handleCloseChat(value.headProposalId)}/></div>
                    </div>
                    { value.lastMessageContent !== undefined ? 
                        <Badge className="badge-newoffer-chat-auth-dash" color="color">ONGOING PROJECT 
                        <NavLink className="chat-project-title-top cursor" tag={Link} to={`/profile/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>{value.projectTopic}</NavLink></Badge> 
                        : value.proposalId === undefined ?
                            <div className="badge-project-cont"><Badge className="badge-newoffer-chat-auth-dash" color="color">NEW OFFER 
                            <NavLink className="chat-project-title-top auth-dash-chat cursor" tag={Link} to={`/profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink></Badge></div>
                    : <Badge color="success">MY BID <NavLink tag={Link} to={`/profile/proposal/&id=${value.proposalId}`}>{value.projectTopic}</NavLink></Badge>
                    }
                </PopoverHeader>
                <PopoverBody className="column-style" style={{ backgroundColor: '#fff'}}> 
                    <div ref={chatContainer} className="chat-box cht-popup" id="chat-div">
                    <ScrollableFeed>
                    {messages.length === 0 ? <MessageBox position={'left'} type={'text'} text={'Still no messages, say Hi!'} date={new Date()} /> : 
                                    messages.map((x: any) => x.isLogMessage === true ? <h4 key={x.id} className="center"><Badge color="secondary">{x.messageText}</Badge></h4> : x.myMessage === false ? (
                                        <MessageBox
                                            key={x.id}
                                            id={x.id}
                                            position={'left'}
                                            type={'text'}
                                            text={x.messageText}
                                            date={new Date(x.dateTimeSent + (x.dateTimeSent.slice(-1) === "Z" ? "" : "Z"))}
                                        />) : (<MessageBox
                                            key={x.id}
                                            id={x.id}
                                            position={'right'}
                                            type={'text'}

                                            text={x.messageText}
                                            date={new Date(x.dateTimeSent + (x.dateTimeSent.slice(-1) === "Z" ? "" : "Z"))}
                                        />))
                                }
                    </ScrollableFeed>
                    </div>
                </PopoverBody>
                { value.bookingStatus !== undefined ? 
                  value.bookingStatus.id === 3 || 
                  value.bookingStatus.id === 5 || 
                  value.proposalStatus === 'Cancelled' || 
                  value.proposalStatus === 'Declined' || 
                  value.proposalStatus === 'Deleted' ? 
                        <div className="center"><span className="badge-text booking-status"><Badge color='primary'>Chat is disabled on this project.</Badge></span></div> : <div>
                            <Form onSubmit={(e) => { e.preventDefault(); if (messageText != '') { SendMessage(dispatch, value.bookingId, value.headProposalId, messageText, alert); setmessageText(''); } }}>
                        <Input placeholder="Your message..." autoComplete="off" id="message-input" className="chat-input" value={messageText} ref={chatInputRef} onChange={handleMessageInput} rightbuttons={
                            <Button className="chat-send" color='primary' type="submit" text='Send'> </Button>
                        }></Input>
                    </Form></div> : <div>
                    <Form onSubmit={(e)=> { e.preventDefault(); if (messageText != '') { SendMessage(dispatch, value.id || value.exactEntityId, value.headProposalId, messageText, alert); setmessageText(''); }}}>
                        <Input placeholder="Your message..." autoComplete="off" id="message-input" className="chat-input bottom" value={messageText} ref={chatInputRef} onChange={handleMessageInput} rightbuttons={
                            <Button className="chat-send" color='primary' type="submit" text='Send'> </Button>
                        }></Input>
                    </Form></div>}
            </UncontrolledPopover>
        </div>)
}

export default ChatComponent;