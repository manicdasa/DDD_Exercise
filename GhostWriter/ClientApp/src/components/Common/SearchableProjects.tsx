import React, { useState, useEffect } from 'react';
import { Col, Form, Label } from 'reactstrap';
import * as yup from 'yup';
import { Formik, Field, ErrorMessage } from 'formik';
import { Button } from 'reactstrap';
import AsyncSelect from 'react-select/async';
import makeAnimated from 'react-select/animated';
import Slider from '@material-ui/core/Slider';
import _ from 'lodash';
import ReactPaginate from 'react-paginate';
import ReactLoading from 'react-loading';
import { BiSearchAlt2 } from 'react-icons/bi';
import { useAlert } from 'react-alert';
import { Expertise, KindOfWork, Languages, HighestDegree } from '../../services/LookupServices';
import { GetProjectInfo } from '../../services/ProjectServices';

let searchProjectSchema = yup.object().shape({
    deadline: yup.date()
})

export const SearchableProjects = ({ match, baseUrl, noProjectsDisplay, projectsMapFunction }: any) => {

    const PER_PAGE = 5;

    const [firstTimeLookup, setFirstTimeLookup] = useState(true);
    const alert = useAlert();
    //selected area of experise
    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<any[]>([]);
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, (firstTimeLookup === true ? 0 : 1500));

    //selected kind of work
    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState<any[]>([])
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, (firstTimeLookup === true ? 0 : 1500));

    //language
    const [selectedLanguageValue, setLanguageSelectedValue] = useState<any[]>([]);
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, (firstTimeLookup === true ? 0 : 1500));

    //highest degree
    const [selectedDegreeValue, setDegreeSelectedValue] = useState<any[]>([]);
    const loadDegreeOptions = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debouncedDegreeOptions = _.debounce(loadDegreeOptions, (firstTimeLookup === true ? 0 : 1500));

    //no of pages for project
    const [pagesFrom, setPagesFrom] = useState(0);
    const [pagesTo, setPagesTo] = useState(1000);

    //the current page
    const [currentPage, setCurrentPage] = useState<number>(0);

    //data recieved from server
    const [projects, setProjects] = useState<any[]>([]);
    const [noOfProjects, setNoOfProjects] = useState<number>(0);
    const [loading, setLoading] = useState<boolean>(false);

    //calculated data
    const pageCount = Math.ceil(noOfProjects / PER_PAGE);

    //data we last searched for
    const [appliedFilters, setAppliedFilters] = useState<any>(
    {
        areaOfExpertise: [],
        kindOfWork: [],
        language: [],
        highestDegree: [],
        pagesFrom: 0,
        pagesTo: 10000,
        deadline: '',
        currentPage: 0
    }) 

    const currentPageData = projects?.map(projectsMapFunction);
            
    useEffect(() => {
        setLoading(true);
        GetProjectInfo(appliedFilters, baseUrl, alert).then((data) => {
            setProjects(data?.items ?? []);
            setNoOfProjects(data?.totalCount ?? 0);
            setLoading(false);
            setFirstTimeLookup(false);
        })
    }, [appliedFilters])

    return (
        <Col sm="12">
            <Formik initialValues={{ deadline: '' }}
                onSubmit={(values) => {
                    setCurrentPage(0);
                    setAppliedFilters({
                        areaOfExpertise: selectedExpertiseValue,
                        kindOfWork: selectedKindOfWorkValue,
                        language: selectedLanguageValue,
                        highestDegree: selectedDegreeValue === null ? "" : selectedDegreeValue,
                        pagesFrom: pagesFrom,
                        pagesTo: pagesTo,
                        deadline: values.deadline,
                        currentPage: 0
                    })
                }}
                validationSchema={searchProjectSchema}>

                {({ handleSubmit }) => {
                    return (
                        <Form className="form-filter-auth-profile" onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                            <div className="row-style form-filter second">
                                <div className="row top-search-options">
                                <div className="select-wrap in">
                                    <label className="kindofwork-lbl">Kind of work</label>
                                    <Field
                                            className=""
                                            placeholder="Kind of work"
                                        as={AsyncSelect}
                                        defaultOptions
                                        isMulti
                                        components={makeAnimated()}
                                        value={selectedKindOfWorkValue}
                                        getOptionLabel={(e: any) => e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedLoadOptions}
                                        onChange={(value: any) => setKindOfWorkSelectedValue(value)}
                                        name="kindOfWorkIds">
                                    </Field>
                                </div>
                                <div className="select-wrap">
                                    <Label for="expertiseAreaIds">Area of expertise</Label>
                                    <Field
                                            className=""
                                            placeholder="Area of expertise"
                                        as={AsyncSelect}
                                        defaultOptions
                                        isMulti
                                        components={makeAnimated()}
                                        value={selectedExpertiseValue}
                                        getOptionLabel={(e: any) => e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedExpertiseOptions}
                                        onChange={(value: any) => setExpertiseSelectedValue(value)}
                                        name="expertiseAreaIds">
                                    </Field>
                                </div>
                                <div className="select-wrap">
                                    <Label for="languageIds">Language</Label>
                                    <Field
                                            className=""
                                            placeholder="Language"
                                        as={AsyncSelect}
                                        defaultOptions
                                        isMulti
                                        components={makeAnimated()}
                                        value={selectedLanguageValue}
                                        getOptionLabel={(e: any) => e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedLanguageOptions}
                                        onChange={(value: any) => setLanguageSelectedValue(value)}
                                        name="languageIds">
                                    </Field>
                                </div>
                                <div className="select-wrap">
                                    <label className="kindofwork-lbl">Highest degree</label>
                                    <Field
                                            className=""
                                            placeholder="Highest degree"
                                        as={AsyncSelect}
                                        defaultOptions
                                        isClearable
                                        components={makeAnimated()}
                                        value={selectedDegreeValue}
                                        getOptionLabel={(e: any) => e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedDegreeOptions}
                                        onChange={(value: any) => setDegreeSelectedValue(value)}
                                        name="highestDegreeIds">
                                    </Field>
                                </div>
                                </div>
                                <div className="row bottom-search-options">
                                <div className="div-input-column select-wrap label-search-filter">
                                    <Label for="deadline">Deadline</Label>
                                    <Field
                                        tooltip="Deadline"
                                        className="label-wrap label-warp-colo"
                                        variant="outlined"
                                        name="deadline"
                                        label="Deadline"
                                        type="date"
                                        autoComplete="off">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="deadline" />
                                </div>
                                    <div className="select-wrap page-num label-search-filter">
                                    <label>Pages From</label>
                                    <Field
                                            className="label-wrap label-warp-colo"
                                        variant="outlined"
                                        name="pagesFrom"
                                        value={pagesFrom}
                                        onChange={(e: any) => setPagesFrom(e.target.value)}
                                        type="number"
                                        autoComplete="off">
                                    </Field>
                                </div>
                                    <div className="select-wrap page-num label-search-filter">
                                    <label>Pages To</label>
                                    <Field
                                            className="label-wrap label-warp-colo"
                                        variant="outlined"
                                        name="pagesTo"
                                        value={pagesTo}
                                        onChange={(e: any) => { setPagesTo(e.target.value) }}
                                        type="number"
                                        autoComplete="off">
                                    </Field>
                                    </div>
                                    <div className="searchauthor-column">
                                        <Button className="srch-btn-btn" color="primary" type="submit" ><BiSearchAlt2 />&nbsp;Search</Button>
                                    </div>
                                    </div>
                                
                            </div>
                        </Form>)
                }}
            </Formik>
            {loading ?
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                    <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF' />
                </div> :
                <div>
                    {noOfProjects === 0 ? noProjectsDisplay : currentPageData}
                    {noOfProjects === 0 ? <div /> : <div><div className="commentBox center">
                        <ReactPaginate
                            previousLabel={"← Previous"}
                            nextLabel={"Next →"}
                            pageRangeDisplayed={5}
                            marginPagesDisplayed={5}
                            pageCount={pageCount}
                            onPageChange={(value) => {
                                setCurrentPage(value.selected);
                                setAppliedFilters({
                                    ...appliedFilters,
                                    currentPage: value.selected
                                });
                            }}
                            forcePage={currentPage}
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
                        <p className="page-count-projects">Page {currentPage + 1} of {pageCount === 0 ? 1 : pageCount}</p></div>}
                </div>}
        </Col>
    );
}

export default SearchableProjects;