import { Rating } from '@material-ui/lab';
import React, { useEffect, useState } from 'react';
import ReactPaginate from 'react-paginate';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useHistory } from 'react-router-dom';
import ReactTooltip from 'react-tooltip';
import { Media, NavLink } from 'reactstrap';
import { useAlert } from 'react-alert';
import ReactLoading from 'react-loading';
import { MdChatBubble, MdLanguage, MdOpenInNew, MdInfo } from 'react-icons/md';

import * as AuthorService from '../../services/AuthorServices';
import AuthorPublicInformation from '../AuthorProfile/AuthorPublicInformation';
import { CreateOffer } from '../../services/ProposalServices';
import { BiArrowBack } from 'react-icons/bi';

export const AssignProjectToAuthor = (props: any) =>
{
    const dispatch = useDispatch();
    const history = useHistory();
    const alert = useAlert();

    const [loaded, setLoaded] = useState<boolean>(true);
    
    const project = useSelector((state: any) => state.assignProjectReducer.assignProjectParams);
    const [people, setPeople] = useState<any>({ totalCount: 0, items: [] });
    
    const PER_PAGE = 5;
    const [page, setPage] = useState(0);
    const pageCount = Math.ceil(people.totalCount / PER_PAGE);

    const handleSearch = async (pageNumber: any) =>
    {
        if(project.id === 0)
        {
            history.goBack();
            return <div/>;
        }
        setPeople({ ...people, items: [] });
        setPage(pageNumber);
        if(project != undefined)
        {    
            var filteredAuthors = await AuthorService.SearchAuthorForAssign(
            {
                ProjectId: project.id,
                KindOfWorkId: project.kindOfWorkDTO.id,
                AreaOfExpertiseId: project.expertiseAreaListDTOs.map((e : any) => e.id),
                LanguageId: project.languageDTO.id,
                HighestDegreeId: project.minimumDegreeDTO.id,
                Deadline: project.deadline,
                NumberOfPages: project.pagesNo,
                PlannedBudget: project.maxBudget,
                Page: pageNumber,
                PageSize: 5
            }, dispatch, alert);
            setPeople(filteredAuthors);
            setLoaded(false);
        }
        else
        {
            setPeople([]);
            history.goBack();
        }
    }

    useEffect(()=>
    {
        handleSearch(0);
    }, []);

    return(
        <div className="row-style">
            <div className="register-user-box customer row">
                <div className="register-signin-left-panel col-sm-12 customer project-content">
                <div className="back-link-cont">
                    <NavLink className="back-link cursor" onClick={()=> { history.goBack(); }}>< BiArrowBack size="24" className="back-icon" /> &nbsp; Back</NavLink>
                </div>
                    <div className="row-style author-info assign-to-author-container">
        
                        <div className="ml-5 " style={{ width: '100%' }}>
                            <h5 className="assign-to-author-proj-info">Project Info</h5>
                        <div className="row-style">
                                <h4>{project.projectTopic}</h4>
                        </div>
                        <div className="row-style">
                                <p className="project-text-details">{project.description}</p>
                        </div>
                        <div className="row-style assign-to-author-details">
                        <div className="column-style mr-5">
                            <p className="profile-subtitle">Areas of expertise:</p>
                            <div className="row-style margin-top">
                                <p className="profile-text">{project.expertiseAreaListDTOs?.map((a: { value: string; description: string }) => a.value).join(', ')}</p>
                            </div>
                        </div>
                        <div className="column-style mr-5">
                            <p className="profile-subtitle">Kind of work: </p>
                            <p className="margin-top profile-text">{project.kindOfWorkDTO?.value}</p>
                        </div>
                        <div className="column-style">
                            <p className="profile-subtitle">Language: </p>
                            <p className="margin-top profile-text">{project.languageDTO?.value}</p>
                        </div>
                        </div>
                    </div>
                </div>
                { loaded ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                    {/* type za react-loading: blank balls bars bubbles cubes cylon spin spinningBubblesspokes */}
                                    {/* https://www.npmjs.com/package/react-loading */}
                                    <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                                </div> 
                                : <div>
                { people.totalCount === 0 ? <div className="center column-style">
                                        <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
                                        <p>There are no authors matching your search parameters.</p>
                                        <p className="nofrs-line"> </p>
                            </div> : 
                people.items?.map((filteredPerson: { picturePath: string, id: number, username: string, highestDegree: { value: string }, expertiseAreas: any, reviewRating: number, reviewCount: number, pricePerPage: number, kindOfWorks: any}) => (
                    <div key={filteredPerson.id} className="author_search_results ml-5">
                        <div className="register-text">
                            
                            <div className="search_author-image col-sm-2">
                                <Media object src={filteredPerson.picturePath} alt="Profile picture" className="image_container"></Media>
                            </div>
                            
                            <div className=" col-sm-5" >
                                <h4> <NavLink className="cursor assign-to-author-author-link" data-tip="" data-for={`${filteredPerson.id}`} data-type="light" onClick={() => { history.push(`/customer-profile/preview-author&id=${filteredPerson.id}`) }}><MdChatBubble color='#4E74DE' className="mt-1" />&nbsp;{filteredPerson.username}</NavLink></h4>
                                <ReactTooltip id={`${filteredPerson.id}`} getContent={() => { return <AuthorPublicInformation value={filteredPerson.id} /> }} />
                                <div className="search_author-text">
                                    <div><span className="assign-to-author-span-bold">Highest degree:&nbsp;</span> {filteredPerson.highestDegree?.value ?? "None"}</div>
                                    <div className="register-text"><span className="assign-to-author-span-bold">Expertise: &nbsp;</span> {filteredPerson.expertiseAreas.slice(0,3).map((expertiseArea: { value: string; }) => expertiseArea.value).join(', ')}</div>
                                    <div className="register-text"><span className="assign-to-author-span-bold">Hockster Abcluss: &nbsp;</span> {filteredPerson.kindOfWorks.slice(0,3).map((workType: { value: string; }) => workType.value).join(', ')}</div> 
                                    {/*<div className="register-text"><span>Language: &nbsp;</span>{filteredPerson.language.map(user => user.name).join(', ')}</div> */}
                                </div>
                            </div>
                            <div className="author_rating_price col-sm-2">
                            <div className="author_rating">
                                    <Rating value={filteredPerson.reviewRating} size="small" precision={0.1} readOnly /><span>({filteredPerson.reviewCount})</span>
                            </div>
                                <div className="author_price">${filteredPerson.pricePerPage}</div>
                        </div>
                            <div className="author_booking_link col-sm-3 assign-to-author-link-cont">
                            <NavLink className="cursor assign-to-author-txt-link" onClick={()=> { CreateOffer(project.id, filteredPerson.id, alert).then(()=> { history.goBack(); }) }}>Assign project to {filteredPerson.username}</NavLink>
                        </div>    
                        </div>
                        <hr></ hr>
                        
                    </div>
                ))} </div> }
                { people.totalCount === 0 ? <div/> : <div><div className="commentBox center">
                        <ReactPaginate
                            previousLabel={"← Previous"}
                            nextLabel={"Next →"}
                            pageRangeDisplayed={PER_PAGE}
                            marginPagesDisplayed={PER_PAGE}
                            pageCount={pageCount}
                            onPageChange={(selected) => { handleSearch(selected.selected);  }}
                            breakClassName={'page-item'}
                            breakLinkClassName={'page-link'}
                            containerClassName={'pagination'}
                            pageClassName={'page-item-class'}
                            pageLinkClassName={'page-link'}
                            previousClassName={'page-item'}
                            previousLinkClassName={'page-link'}
                            nextClassName={'page-item'}
                            nextLinkClassName={'page-link'}
                            activeClassName={'active'}
                        />
                    </div>
                    <p className="page-numbers-">Page {page + 1} of {pageCount === 0 ? 1 : pageCount}</p></div>}
            </div>
        </div>
        </div>)
}

export default AssignProjectToAuthor;