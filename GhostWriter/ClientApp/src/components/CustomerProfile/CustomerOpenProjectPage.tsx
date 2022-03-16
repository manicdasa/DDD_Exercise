import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { NavLink, Button, Card, CardBody } from 'reactstrap';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import dayjs from 'dayjs';
import ReactLoading from 'react-loading';
import { Media } from 'reactstrap';
import { BiArrowBack, BiEditAlt } from "react-icons/bi";
import { MdAssignment, MdChatBubble } from 'react-icons/md';
import { ImBooks } from "react-icons/im";
import { RiTimerFill } from "react-icons/ri";
import { IoBookSharp } from "react-icons/io5";
import { BiInfoCircle } from "react-icons/bi";
import NumberFormat from 'react-number-format';
import { RiBroadcastFill, RiFolderSharedFill } from "react-icons/ri";
import { TiCancel } from "react-icons/ti";
import { useAlert } from 'react-alert';
import type { RootState } from '../../../src/store/store';
import { RiDeleteBin6Line } from 'react-icons/ri';
import { Popup } from 'react-chat-elements'
import Switch from "react-switch";

import { GetProjectDetails, DropProposalCustomerOnProfile, AcceptProposalCustomerOnProfile, CancelOfferCustomer } from '../../services/CustomerServices';
import { ActionCreators} from '../../store/CustomerReducer'
import { BidsOffersComponent } from '../Common/BidsOffersComponent';
import { DeleteProject } from '../../services/ProjectServices';
import { ActionCreatorsForAssign } from '../../store/AssignProjectReducer';
import { ChangeBroadcastInfo } from '../../services/ProjectServices';
import { ActionCreatorsForSidePanel } from '../../store/SidePanelReducer';
import { DropBidAuthor } from '../../services/ProfileServices';
import ReactPaginate from 'react-paginate';
import { BsFileEarmarkCheck } from 'react-icons/bs';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';



export const CustomerOpenProposalPage = ({ openChats, numberOfOpenChats}: any) =>
{
    const queryString = require('query-string');
    const stateParams : any = queryString.parse(window.location.pathname);

    const alert = useAlert();

    const dispatch = useDispatch();

    const history = useHistory();

    const [deleteProjectPopup, setdeleteProjectPopup] = useState(false);
    const closeDeletePopup = () =>
    {
        setdeleteProjectPopup(false);
    }
    
    let popupContent =
        <div className="row-style center">
            <Button color="primary" className="mr-5" onClick={()=> setdeleteProjectPopup(false)}>Decline</Button>
            <Button color="secondary" onClick={()=> DeleteProject(stateParams.id, alert, history, closeDeletePopup)}>Confirm</Button>
        </div>

    const liveBroadcast = useSelector((state: RootState) => state.customerReducer.proposalDetailsCustomer); 
    const loadingValue = useSelector((state: RootState) => state.customerReducer.proposalDetailsLoadingValueCustomer); 

    const [broadcasted, setBroadcasted] = useState(liveBroadcast.isPublished);

    useEffect(()=>{ setBroadcasted(liveBroadcast.isPublished) },[liveBroadcast])

    const [currentPageOffers, setCurrentPageOffers] = useState<number>(0);
    const PER_PAGE_OFFERS = 3;
    const pageCountOffers = Math.ceil(liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Offer').length / PER_PAGE_OFFERS);
    const offsetOffers = currentPageOffers * PER_PAGE_OFFERS;

    const [currentPageBids, setCurrentPageBids] = useState<number>(0);
    const PER_PAGE_BIDS = 3;
    const pageCountBids = Math.ceil(liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Bid').length / PER_PAGE_BIDS);
    const offsetBids = currentPageBids * PER_PAGE_BIDS;

    const [readOnly, setReadOnly] = useState(false);

    const handleChange = (checked : any) =>
    {
        ChangeBroadcastInfo(stateParams.id, checked, setBroadcasted, alert);
    }  

    useEffect(()=>
    {
        GetProjectDetails(dispatch, stateParams.id, alert);
        dispatch(ActionCreators.setProposalDetailsCustomerLoadingValue(true));
    }, [stateParams.id, history.location.key])

    
    useEffect(()=> {
        if(liveBroadcast.projectStatus != 1)
        {
            setReadOnly(true);
        }
        else if(liveBroadcast.projectStatus === 1)
        { 
            setReadOnly(false);
        }
    }, [liveBroadcast])

    return (
        <div className="row-style">
              { loadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                                          <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                               </div>  :
            <div className="register-user-box customer row">
                    <div className="register-signin-left-panel col-sm-9 customer project-content">
                <div className="back-link-cont row-style">
                            <NavLink className="back-link txt-link-ghw" tag={Link} to={"/customer-profile"}><BiArrowBack size="24" className="back-icon txt-link-ghw"/> &nbsp; Back</NavLink>
                            <div className={(readOnly === true ? 'overlap-readonly modify-project-link' : 'modify-project-link')}><NavLink className="back-link txt-link-ghw" tag={Link} to={{ pathname: "/customer-profile/project/modify", projectProps: liveBroadcast }}><BiEditAlt size="24" className="back-icon txt-link-ghw" /> &nbsp; Modify Project</NavLink> </div>
                            <div className={(readOnly === true ? 'overlap-readonly assign-project-link' : 'assign-project-link')}><NavLink className="back-link txt-link-ghw cursor" style={{ float: 'right' }} onClick={()=> { dispatch(ActionCreatorsForAssign.setAssignParams(liveBroadcast)); history.push("/customer-profile/assign-project"); }}><RiFolderSharedFill size="24" className="back-icon txt-link-ghw" />&nbsp;Assign Project</NavLink> </div>
                </div>
                <h4>{liveBroadcast.projectTopic}</h4>
                <div className="sub-cont row">
                     <div className="sub-txt-bold">Kind of work:</div> &nbsp; <div className="sub-txt-lite"> {liveBroadcast.kindOfWorkDTO.value}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Expertise area:</div> &nbsp;<div className="sub-txt-lite">{liveBroadcast.expertiseAreaListDTOs?.map((u: { value: string; description: string }) => u.value).join(', ')}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Language:</div> &nbsp;<div className="sub-txt-lite"> {liveBroadcast.languageDTO.value}</div>
                </div>
                <br></br>
                <h4>Project description</h4>
                <p>{liveBroadcast.description}</p>
                        
                        
              { readOnly === true ? 
                            <Card className=" mt-3 cursor row-style final-docs-card read-only-card">
                                <CardBody className=" final-doc-container read-only-cardbody">
                                    <div className="card-text-final read-only-cardtext">
                                        <div className="icon-info-readonly-cont"><TiCancel className="svg-readonly-cancel" /> </div> <div className="text-readonly-cont" >This project is no longer active for bids/offers. Here you can only preview the proposal history.</div>
                                        </div>
                                        </CardBody>
                                    </Card> : <div />}
              </div>

              <div className="register-signin-right-panel col-sm-3 column-style project-info">
                        <h4>Requirements: </h4>
                        <div className="column-style mrt-15">
                          <p className="pages-txt-cont" ><span className="bold-text">Number of pages: </span>{liveBroadcast.pagesNo}</p>
                          <p className="pages-txt-cont" ><span className="bold-text">Deadline: </span>{dayjs(liveBroadcast.deadline).format('DD.MM.YYYY').toString()}</p>
                            <p className="pages-txt-cont" ><span className="bold-text">Max budget: </span>&#8364;&nbsp;<NumberFormat value={liveBroadcast.maxBudget} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                            <label className="center mt-3 column-style">
                                <span className="bold-text live-broadcast-switch">Live Broadcast</span>
                                <div className={(readOnly === true ? 'overlap-readonly col-sm-3 docs-cont bid-proj' : '')}><Switch disabled={readOnly === true ? true : false} onChange={(v: any) => handleChange(v)} checked={broadcasted} /> </div>
                            </label>
                            <div className={(readOnly === true ? 'center column-style readonly-btn-cont' : 'center column-style')}><Button disabled={readOnly === true ? true : false} className="button-edit mb-2 delete-project" onClick={() => setdeleteProjectPopup(true)}><RiDeleteBin6Line className="edit-icon" />&nbsp;Delete project</Button></div>
                        </div>

    
                
                    </div>
                    <div className="col-sm-9 chat-cont">
                      </div>
                    <div className={(readOnly === true ? 'overlap-readonly col-sm-3 docs-cont bid-proj' : 'col-sm-3 docs-cont bid-proj')}>
                        <div>
                            { liveBroadcast.proposalDetailsDTOs.length === 0 ? 
                                <div className="center column-style">
                                    <div className="img-noclosedproj">
                                        <Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media>
                                    </div>
                                    <p className="no-docs-txt">You don't have any new bids or offers.</p>
                                    <p className="nofrs-line"> </p>
                                </div> 
                                : <div className="column-style bids-offers-section">
                                    <div className="register-signin-right-panel col-sm-3 column-style project-info bids-section">
                                        <h4 className="bids-title-section">Bids:</h4> 
                                    {liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Bid').length === 0 ?
                                        <div className="center column-style no-bids-txt-img">
                                            <div className="img-noclosedproj">
                                                <Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media>
                                            </div>
                                            <p className="no-docs-txt">You don't have any new bids.</p>
                                            <p className="nofrs-line"> </p>
                                        </div> : 
                                        liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Bid').slice(offsetBids, offsetBids + PER_PAGE_BIDS).map((value: any) => 
                                        <div key={value.id}>
                                                <div id={'offer' + value.id} className="offer-card bids-section example-enter">
                                                    <div className="p-username" onClick={() => { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}>{/*<div className="user-icon-bids"><MdChatBubble color='#4E74DE' />&nbsp;</div>*/}<p className="date-created-txt-bids"><span className="bold-text">Author:</span></p><div className="">&nbsp;@{value.authorUsername}</div></div>
                                                <h6 className="h-title">{value.projectTopic}</h6>
                                                    <div className="date-created-bids">
                                                        <p className="date-created-txt-bids"><span className="bold-text">Date created:</span></p><p>{dayjs(value.dateCreated).format('DD.MM.YYYY').toString()}</p>
                                                    </div>
                                                    <div className="bid-offer-cont">
                                                        <div className="div-offer bids-section-project-value"><p className="project-values-bids">Project Offer:&nbsp;</p><p className="project-value-nub-bids">&#8364;&nbsp;<NumberFormat value={value.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p></div>
                                                        <div className="btns-acc-dec-bids-cont">
                                                            <Button id="accept-proposal-button" className="btns-sidebar" color="primary" onClick={()=>AcceptProposalCustomerOnProfile(dispatch, value.id, value, alert).then(()=>history.push('/customer-profile/refresh'))}>Accept Bid</Button>
                                                        </div>
                                                        <div className="btns-acc-dec-bids-cont">
                                                        <Button className="btns-sidebar ml-3 btn btn-secondary open-chat" onClick={() => 
                                                        { 
                                                        if (openChats.length !== numberOfOpenChats) {
                                                            if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) 
                                                            {       
                                                                dispatch(ActionCreatorsForChatComponent.setOpenChats({...value, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id}))
                                                            }
                                                        }
                                                        else {
                                                            dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                                                            if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) 
                                                            {
                                                                dispatch(ActionCreatorsForChatComponent.setOpenChats({...value, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id}))
                                                            }
                                                        }
                                                        }}>Negotiate</Button>
                                                        </div>
                                                        <div id={`button-row${value.id}`} className="row-style mt-3 btns-acc-dec-bids-cont">
                                                            <Button className="btns-sidebar decline-bid-bids-section" onClick={()=>DropProposalCustomerOnProfile(dispatch, value.id, alert).then(()=>history.push('/customer-profile/refresh'))}>Decline</Button> 
                                                        </div>
                                                    
                                                    
                                                    <p id={`messages-accept${value.id}`} style={{display: 'none', color: 'green'}}>You accepted {value.authorUsername} offer.</p>
                                                    <p id={`messages-decline${value.id}`} style={{display: 'none', color: 'red'}}>You declined {value.authorUsername} offer.</p>
                                                </div>
                                                </div>
                                        </div>
                                        )
                                        }
                                        {liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Bid').length === 0 ? <div>
                                            <div className="commentBox pagination-bids-section-cont">
                                                <ReactPaginate
                                                    previousLabel={"← Previous"}
                                                    nextLabel={"Next →"}
                                                    pageRangeDisplayed={5}
                                                    marginPagesDisplayed={5}
                                                    pageCount={pageCountBids}
                                                    onPageChange={(value) => { setCurrentPageBids(value.selected); }}
                                                    breakClassName={'page-item'}
                                                    breakLinkClassName={'page-link'}
                                                    containerClassName={'pagination bids-cont'}
                                                    pageClassName={'page-item-class'}
                                                    pageLinkClassName={'page-link bids'}
                                                    previousClassName={'page-item prev-bid-item'}
                                                    previousLinkClassName={'page-link prev-bids'}
                                                    nextClassName={'page-item next-bid-item'}
                                                    nextLinkClassName={'page-link next-bids'}
                                                    activeClassName={'active active-bids'}
                                                />
                                            </div>
                                        <p className="page-count-sidebar1 bid-page-info">Bid {currentPageBids + 1} of {pageCountBids === 0 ? 1 : pageCountBids}</p></div> : <div/> }
                                    </div>
                                    <div className="offers-container-details">
                                        <h4 className="bids-title-section">Offers:</h4> 
                                    {liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Offer').length === 0 ? 
                                        <div className="center column-style">
                                            <div className="img-noclosedproj">
                                                <Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media>
                                            </div>
                                            <p className="no-docs-txt">You don't have any new offers.</p>
                                            <p className="nofrs-line"> </p>
                                        </div> 
                                        : liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Offer').slice(offsetOffers, offsetOffers + PER_PAGE_OFFERS).map((value: any) => 
                                        <div key={value.id}>    
                                        <div className="mt-3 example-enter">
                                            <div  className="bid-card offers-project-details-customer bids-customer">
                                                        {/*<MdChatBubble color='#4E74DE' />&nbsp;*/}<p className="date-created-txt-bids"><span className="bold-text">Author:</span></p><div className="offers-section-username">@{value.authorUsername}</div>
                                                        {/* <h6 className="h-title"><NavLink tag={Link} to={`/profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink></h6>*/}
                                                        <div className="date-created-bids offers-deadline-created">
                                                            <p className="date-created-txt-bids"><span className="bold-text">Date created:</span></p><p>{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                                                </div>
                                                        {/*<div className="date-created-bids offers-deadline-created"><p className="date-created-txt-bids"><span className="bold-text">Language: </span></p><p className="lang-value"> {value.languageDTO?.value}</p></div>*/}
                                                        <div className="bid-offer-cont"> <div className="div-offer bids-section-project-value"><p className="project-values-bids">Project value:</p><p className="project-value-nub-bids"> <span className="lang-value"> &#8364;&nbsp;<NumberFormat value={value.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> </span></p></div></div>

                                                        <div className="btns-acc-dec-bids-cnt mt-4">
                                                            <Button className="btns-sidebar btn btn-secondary open-chat" onClick={() => {
                                                                if (openChats.length !== numberOfOpenChats) {
                                                                    if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) {
                                                                        dispatch(ActionCreatorsForChatComponent.setOpenChats({ ...value, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id }))
                                                                    }
                                                                }
                                                                else {
                                                                    dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                                                                    if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) {
                                                                        dispatch(ActionCreatorsForChatComponent.setOpenChats({ ...value, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id }))
                                                                    }
                                                                }
                                                            }}>Negotiate</Button>
                                                        </div> 
                                                        <div className="btns-acc-dec-bids-cont offers-sect"> <div id={`button-row-bid${value.id}`} className=""> 
                                                            <Button className="cancel-offer-btn offers-section" onClick={()=> CancelOfferCustomer(dispatch, value.id, alert).then(() => { history.push('/profile/refresh') })}>Cancel Offer</Button>
                                                        </div>
                                                             
                                                        </div>
                                                    </div>
                                                </div>    
                                        </div>
                                        )}
                                         {liveBroadcast.proposalDetailsDTOs.filter((value: any) => value.proposalType === 'Offer').length === 0 ? <div>
                                            <div className="commentBox pagination-bids-section-cont">
                                                <ReactPaginate
                                                        previousLabel={"← Previous"}
                                                        nextLabel={"Next →"}
                                                        pageRangeDisplayed={5}
                                                        marginPagesDisplayed={5}
                                                        pageCount={pageCountOffers}
                                                        onPageChange={(value) => { setCurrentPageOffers(value.selected); }}
                                                        breakClassName={'page-item'}
                                                        breakLinkClassName={'page-link'}
                                                    containerClassName={'pagination bids-cont'}
                                                        pageClassName={'page-item-class'}
                                                    pageLinkClassName={'page-link bids'}
                                                    previousClassName={'page-item prev-bid-item'}
                                                    previousLinkClassName={'page-link prev-bids'}
                                                    nextClassName={'page-item next-bid-item'}
                                                    nextLinkClassName={'page-link next-bids'}
                                                    activeClassName={'active active-bids'}
                                                    />
                                                </div>
                                            <p className="page-count-sidebar1 bid-page-info">Offer {currentPageOffers + 1} of {pageCountOffers === 0 ? 1 : pageCountOffers}</p></div> : <div/> }
                                        </div>
                                </div>
                            }
                        </div>
                    </div>
                    
                    
                    <Popup
                        show={deleteProjectPopup}
                        header='Are you sure you want to delete this project?'
                        headerButtons={[{
                            type: 'transparent',
                            color: 'black',
                            text: 'X',
                            onClick: () => {
                                setdeleteProjectPopup(false);
                            }
                        }]}                        
                        renderContent={() => { return popupContent }}
                     />
                </div>
            } 
        </div>)
}

export default CustomerOpenProposalPage;