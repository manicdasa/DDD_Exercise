import React, { useState, useEffect } from 'react';
import AsyncSelect from 'react-select/async';
import { useDispatch, useSelector } from 'react-redux';
import Rating from '@material-ui/lab/Rating';
import * as yup from 'yup';
import { Formik, Field, ErrorMessage } from 'formik';
import { Link, useHistory } from 'react-router-dom';
import ReactLoading from 'react-loading';
import ReactPaginate from 'react-paginate';
import { Media, Button, Form, NavLink } from 'reactstrap';
import ReactTooltip from 'react-tooltip';
import { useAlert } from 'react-alert';
import '../styles/SearchAuthor.css'
import { ActionCreators } from '../store/SearchAuthorReducer';
import * as AuthorService from '../services/AuthorServices';
import { Languages, Expertise, KindOfWork, HighestDegree } from '../services/LookupServices';
import makeAnimated from 'react-select/animated';
import _ from 'lodash';
import AuthorPublicInformation from './AuthorProfile/AuthorPublicInformation';
import type { RootState } from '../../src/store/store';


let searchSchema = yup.object().shape({
    number: yup.number().positive('Page number must be above 0.'),
    deadline: yup.date(),
    budget: yup.number().positive('Budget must be above 0.').nullable()
})

interface Expertise {
    value: string
}

export const SearchAuthor = () =>
{
    const dispatch = useDispatch();
    const [activeTab, setActiveTab] = useState('1');

    const toggle = (tab: React.SetStateAction<string>) => {
        if(activeTab !== tab) setActiveTab(tab);
    }
    const alert = useAlert();
    const history = useHistory();

    const value = useSelector((state: RootState) => state.searchAuthorReducer.authorParams);

    const [requestBookingValues, setRequestBookingValues] = useState({});

    const loadingValue = useSelector((state: RootState) => state.searchAuthorReducer.loadingValue);

    const [loaded, setLoaded] = useState<boolean>(false);

    const [selectedLanguageValue, setSelectedLanguageValue] = useState<{name: string, value: number}>(value.language);
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, 1500);

    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<{ name: string, value: number }>(value.expertise);
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, 1500);

    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState<{ name: string, value: number }>(value.kindOfWork)
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, 1500);

    const [selectedHighestDegree, setSelectedHighestDegree] = useState<{ name: string, value: number }>(value.highestDegree)
    const loadOptionsHighestDegree = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debounceHighestDegree = _.debounce(loadOptionsHighestDegree, 1500);

    const [people, setPeople] = useState<any>({ totalCount: 0, items: [] });

    const PER_PAGE = 10;
    const [page, setPage] = useState(0);
    const pageCount = Math.ceil(people.totalCount / PER_PAGE);

    useEffect(() => {
        if(value.kindOfWork != undefined && value.language != undefined && value.expertise != undefined)
        { 
            setRequestBookingValues({kindOfWork: 
                                        { "name": value.kindOfWork.name,
                                          "value": value.kindOfWork.value 
                                        }, 
                                    language:
                                        { "name": value.language.name,
                                          "value": value.language.value 
                                        }, 
                                    expertise: 
                                        { "name": value.expertise.name,
                                          "value": value.expertise.value
                                    },
                                    highestDegreeId: 
                                    {
                                        "name": value.highestDegree.name,
                                        "value": value.highestDegree.value
                                    },
                                    budget: value.budget,
                                    deadline: value.deadline,
                                    number: value.number
                                    });
        }
        if (!loaded) {
            setLoaded(true);
            handleSearch({ ...value, page: 0 });
        }
    }, []);

    const handleSearch = async (values: { page: number, deadline: Date, number: number, budget: number }) => {

        setPeople({ ...people, items: [] });
        dispatch(ActionCreators.setLoadingValue(true)); 
        setPage(values.page);
        if (selectedKindOfWorkValue != undefined && selectedExpertiseValue != undefined && selectedLanguageValue != undefined && selectedHighestDegree != undefined && values.deadline != undefined && values.number != undefined) {
            var filteredAuthors = await AuthorService.SearchAuthor(
                {
                    KindOfWorkId: selectedKindOfWorkValue.value,
                    AreaOfExpertiseId: selectedExpertiseValue.value,
                    LanguageId: selectedLanguageValue.value,
                    HighestDegreeId: selectedHighestDegree.value,
                    Deadline: values.deadline,
                    NumberOfPages: values.number,
                    PlannedBudget: values.budget,
                    Page: values.page,
                    PageSize: PER_PAGE
                } as AuthorService.AuthorSearchParams, dispatch, alert);
            setPeople(filteredAuthors);
        }
        else
            setPeople([]);
            dispatch(ActionCreators.setLoadingValue(false));
    }

    return(
        <div className="search-author-box row">
            <div className="search-author-left-panel col-sm-4">
                <h3>You are almost there!</h3>
                <div className="left-panel-text">
                <p>On your right you'll find the matching profiles to your request.</p>
                    <p>To make it easier for you to find the perfect author for your work, we recommend that you create your project anonymously and free of charge in just a few steps.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/toBeChangedSearchAuthor.png" alt="Typewriter image"></Media>
                    </div>
            </div>

            <div className="search-author-right-panel col-sm-8">

                <Formik 
                    initialValues={{ number: value.number, deadline: value.deadline, budget: value.budget }}
                    onSubmit={
                        (values) => 
                        {   
                            setRequestBookingValues({kindOfWork: 
                                                        { "name": selectedKindOfWorkValue.name,
                                                          "value": selectedKindOfWorkValue.value 
                                                        }, 
                                                    language:
                                                        { "name": selectedLanguageValue.name,
                                                          "value": selectedLanguageValue.value 
                                                        }, 
                                                    expertise: 
                                                        { "name": selectedExpertiseValue.name,
                                                          "value": selectedExpertiseValue.value
                                                        },
                                                    highestDegreeId:
                                                        { "name": selectedHighestDegree.name,
                                                          "value": selectedHighestDegree.value
                                                        },
                                                        budget: values.budget === '' ? undefined : values.budget,
                                                        deadline: values.deadline,
                                                        number: values.number
                                                    });

                            dispatch(ActionCreators.setAuthorParams({ ...values, kindOfWork: selectedKindOfWorkValue, expertise: selectedExpertiseValue, language: selectedLanguageValue, highestDegree: selectedHighestDegree })); 
                            handleSearch({ ...values, page: 0, budget: values.budget === "" ? undefined : values.budget });
                        }}
                    validationSchema={searchSchema}>

                    {({ handleSubmit }) => 
                    <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                        <div className="search-bar">
                            <div className="searchauthor-row">
                                <div className="searchauthor-column col-sm-6">
                                    <div className="label-search-bar">Kind of work</div>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
                                        components={makeAnimated()}
                                        value={selectedKindOfWorkValue}
                                        getOptionLabel={(e: { name: string }) => e.name}
                                        getOptionValue={(e: { value: number }) => e.value}
                                        loadOptions={debouncedLoadOptions}
                                        onChange={(value: { name: string, value: number }) => setKindOfWorkSelectedValue(value)}
                                        name="kindOfWorkIds">
                                    </Field>
                                </div>
                                <div className="searchauthor-column col-sm-6">
                                    <div className="label-search-bar">Area of expertise</div>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
                                        components={makeAnimated()}
                                        value={selectedExpertiseValue}
                                        getOptionLabel={(e: { name: string }) => e.name}
                                        getOptionValue={(e: { value: number }) => e.value}
                                        loadOptions={debouncedExpertiseOptions}
                                        onChange={(value: { name: string, value: number }) => setExpertiseSelectedValue(value)}
                                        name="expertiseAreaIds">
                                    </Field>
                                </div>                          
                            </div>

                            <div className="searchauth-row"></div>

                            <div className="searchauthor-row ">
                                <div className="searchauthor-column col-sm-6">
                                    <div className="label-search-bar">Language</div>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
                                        components={makeAnimated()}
                                        value={selectedLanguageValue}
                                        getOptionLabel={(e: { name: string }) => e.name}
                                        getOptionValue={(e: { value: number }) => e.value}
                                        loadOptions={debouncedLanguageOptions}
                                        onChange={(value: {name: string, value: number}) => setSelectedLanguageValue(value)}
                                        name="languageIds">
                                    </Field>
                                </div>
                                <div className="searchauthor-column col-sm-6">
                                    <div className="label-search-bar">Minimum Degree</div>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
                                        components={makeAnimated()}
                                        value={selectedHighestDegree}
                                        getOptionLabel={(e: { name: string }) => e.name}
                                        getOptionValue={(e: { value: number }) => e.value}
                                        loadOptions={debounceHighestDegree}
                                        onChange={(value: { name: string, value: number }) => setSelectedHighestDegree(value)}
                                        name="highestdegreeIds">
                                    </Field>
                                </div>
                            </div>
                                <div className="searchauthor-row searchauth-row">
                                <div className="searchauthor-column-3 col-sm-4">
                                    <div className="label-search-bar-b">Number of pages</div>
                                    <Field
                                        variant="outlined" 
                                        name="number" 
                                        label="Number of pages" 
                                        type="number"
                                        autoComplete="off">      
                                    </Field>
                                <ErrorMessage component="p" className="field-colorchange ml-3" name="number" />
                                </div>
                                <div className="searchauthor-column-3 col-sm-4">
                                    <div className="label-search-bar-b">Deadline</div>
                                    <Field
                                        variant="outlined" 
                                        name="deadline" 
                                        label="Deadline" 
                                        type="date"
                                        autoComplete="off">      
                                    </Field>
                                </div>
                                <div className="searchauthor-column-3 col-sm-4">
                                    <div className="label-search-bar-b">Price per page (Optional)</div>
                                    <Field
                                        variant="outlined" 
                                        name="budget" 
                                        label="Budget" 
                                        type="number"
                                        autoComplete="off">      
                                    </Field>
                                <ErrorMessage component="p" className="field-colorchange ml-3" name="budget" />
                                </div>
                            </div>
                                <div className="searchauthor-column button">
                                    <Button color="primary" type="submit">Refine search</Button>
                                </div>
                        </div>
                    </Form>}
            </Formik>
            <div>  
                { loadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                    {/* type za react-loading: blank balls bars bubbles cubes cylon spin spinningBubblesspokes */}
                                    {/* https://www.npmjs.com/package/react-loading */}
                                    <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                                </div> 
                                : <div>
                { people.length === 0 ? <div className="center column-style">
                                        <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
                                        <p>There are no authors matching your search parameters.</p>
                                        <p className="nofrs-line"> </p>
                            </div> : people.items?.map((filteredPerson: { picturePath: string, id: number, username: string, highestDegree: { value: string }, expertiseAreas: Array<Expertise>, reviewRating: number, reviewCount: number, pricePerPage: number, kindOfWorks: Array<Expertise>}) => (
                    <div key={filteredPerson.id} className="author_search_results col-sm-12">
                        <div className="register-text">
                            
                            <div className="search_author-image col-sm-2">
                                <Media object src={filteredPerson.picturePath} alt="Profile picture" className="image_container"></Media>
                            </div>
                            
                            <div className=" col-sm-5" >
                                <h4><NavLink className="cursor" data-tip="" data-for={`${filteredPerson.id}`} data-type="light" to="#">{filteredPerson.username}</NavLink></h4>
                                <ReactTooltip id={`${filteredPerson.id}`} getContent={() => { return <AuthorPublicInformation value={filteredPerson.id} /> }} />
                            <div className="search_author-text">
                                    <div><span>Highest degree:&nbsp;</span> {filteredPerson.highestDegree?.value ?? "None"}</div>
                                    <div className="register-text"><span>Expertise: &nbsp;</span> {filteredPerson.expertiseAreas.slice(0,3).map((expertiseArea: { value: string; }) => expertiseArea.value).join(', ')}</div>
                                    <div className="register-text"><span>Hockster Abcluss: &nbsp;</span> {filteredPerson.kindOfWorks.slice(0,3).map((workType: { value: string; }) => workType.value).join(', ')}</div> 
                                    {/*<div className="register-text"><span>Language: &nbsp;</span>{filteredPerson.language.map(user => user.name).join(', ')}</div> */}
                                </div>
                            </div>
                            <div className="author_rating_price col-sm-2">
                            <div className="author_rating">
                                    <Rating value={filteredPerson.reviewRating} size="small" precision={0.1} readOnly /><span>({filteredPerson.reviewCount})</span>
                            </div>
                                <div className="author_price">Price per page: ${filteredPerson.pricePerPage}</div>
                        </div>
                        <div className="author_booking_link col-sm-3">
                            <NavLink tag={Link} to={{ pathname:"/request-booking", userProps: filteredPerson, valueProps: requestBookingValues}}>Request a booking</NavLink>
                        </div>    
                        </div>
                        <hr></ hr>
                        
                    </div>
                ))}</div> }
                { people.length === 0 ? <div/> : <div><div className="commentBox center">
                        <ReactPaginate
                            previousLabel={"← Previous"}
                            nextLabel={"Next →"}
                            pageRangeDisplayed={PER_PAGE}
                            marginPagesDisplayed={PER_PAGE}
                            pageCount={pageCount}
                            onPageChange={(selected) => { handleSearch({ ...value, page: selected.selected });  }}
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
        </div>  
    )
}

export default SearchAuthor;
