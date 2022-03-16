import React, { useEffect, useRef, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Badge, Button, Form, Input, NavLink, PopoverBody, PopoverHeader, UncontrolledPopover } from 'reactstrap';
import { MessageBox } from 'react-chat-elements';
import { MdClose } from 'react-icons/md';
import ScrollableFeed from 'react-scrollable-feed'

import { useAlert } from 'react-alert';

import { invokeConnection } from '../Helpers/SignalRMiddleware';
import { GetAllMessagesForProfileChat, SendMessage } from '../../services/ProfileServices';
import { Link, useHistory } from 'react-router-dom';
import { RiUserVoiceLine, RiMessage3Line } from 'react-icons/ri';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';

export const CustomerChatComponent = ({ openChats,  value } : any) => 
{   
    const history = useHistory();

    const dispatch = useDispatch();

    const messages = useSelector((state: any) => state.authorActiveProjectsReducer.messages.filter((x: any) => x.headProposalId === value.headProposalId));

    const handleCloseChat = (headProposalId: number) => 
    {
        dispatch(ActionCreatorsForChatComponent.closeOpenChat(headProposalId));
    }

    const [messageText, setmessageText] = useState('');
    const chatContainer: any = React.createRef();
    
    const chatInputRef = useRef<any>(null);

    const handleMessageInput = (event: any) => {
        setmessageText(event.target.value);
    }

    const alert = useAlert();

    useEffect(()=>
    {
        GetAllMessagesForProfileChat(dispatch, value.headProposalId, alert);
        invokeConnection(value.headProposalId);
    },[])

    return(
        <div>
            <Button id={`PopoverClick`+value.headProposalId} type="button" className="messages-button chat-button-bottom" style={{ border: '1px solid rgb(93, 169, 255)' }}>
                <div className="chat-bar-text">< RiUserVoiceLine className="icon-cht-button" onClick={()=> { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}/> {value.authorUsername}</div>
            </Button>
            <UncontrolledPopover id="chat-popover" trigger="click" placement="top" target={`PopoverClick`+value.headProposalId}>
                <PopoverHeader>
                    <div className="row-style">
                        <div className="title-chat"><div className="chat-icon-cont"><RiMessage3Line className="icon-cht-header" /></div><div className="chat-username-top">{value.authorUsername}</div> </div>
                        <div><MdClose color="#4E74DE" className="my-cursor-style close-icon-chat" onClick={() => handleCloseChat(value.headProposalId)}/></div>
                    </div>
                    { value.lastMessageContent !== undefined ?
                        <NavLink className="cursor" tag={Link} to={`/customer-profile/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>{value.projectTopic}</NavLink>
                        : <NavLink className="chat-project-title-top cursor" tag={Link} to={`/customer-profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink>}
                    
                    { value.lastMessageContent !== undefined ? 
                        <Badge color="info" className="info-chat-top">ONGOING PROJECT 
                        <NavLink className="cursor" tag={Link} to={`/customer-profile/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>{value.projectTopic}</NavLink> </Badge>
                        : <Badge color="info" className="info-chat-top">OFFER  { /*<NavLink tag={Link} to={`/customer-profile/project/id=${value.projectId}`}>{value.projectTopic}</NavLink>*/}</Badge>
                    }
                </PopoverHeader>
                <PopoverBody className="column-style" style={{ backgroundColor: '#fff'}}> 
                <div ref={chatContainer} className="chat-box cht-popup" id="chat-div">
                    <ScrollableFeed>
                    {messages.length === 0 ? <MessageBox position={'left'} type={'text'} text={'Still no messages, say Hi!'} date={new Date()} />
                                :
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
                { value.bookingStatus !== undefined ? value.bookingStatus.id === 3 || value.bookingStatus.id === 5 ||
                  value.proposalStatus === 'Cancelled' || 
                  value.proposalStatus === 'Declined' || 
                  value.proposalStatus === 'Deleted' ? 
                    <div className="center"><span className="badge-text booking-status"><Badge color='primary'>Chat is disabled on this project.</Badge></span></div> : <div>
                    <Form onSubmit={(e)=> { e.preventDefault(); if (messageText != '') { SendMessage(dispatch, value.bookingId, value.headProposalId, messageText, alert); setmessageText(''); }}}>
                            <Input placeholder="Your message..." autoComplete="off" id="message-input" className="chat-input bottom" value={messageText} ref={chatInputRef} onChange={handleMessageInput} rightbuttons={
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

export default CustomerChatComponent;