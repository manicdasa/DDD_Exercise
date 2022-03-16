import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Button, Form } from 'reactstrap'
import AsyncSelect from 'react-select/async';
import * as yup from 'yup'
import { Formik, Field, ErrorMessage } from 'formik';
import { BiSearchAlt2 } from 'react-icons/bi';
import { useHistory } from 'react-router-dom';
import dayjs from 'dayjs';
import { useAlert } from 'react-alert';
import { Languages, Expertise, KindOfWork, HighestDegree } from '../services/LookupServices';
import { HowDoesThisWork } from './HowDoesThisWork';
import { ActionCreators } from '../store/SearchAuthorReducer';
import '../styles/LandingPage.css'
import _ from 'lodash';
import makeAnimated from 'react-select/animated';
import type { RootState } from '../../src/store/store';
import CustomerAuthorLandingInfo from './Layout/CustomerAuthorLandingInfo';

let homeSchema = yup.object().shape({
    kindOfWork: yup.string(),
    expertise: yup.string(),
    language: yup.string(),
    highestDegreeId: yup.string(),
    number: yup.number().positive('Page number must be above 0.'),
    deadline: yup.date(),
    budget: yup.number().positive('Budget must be above 0.').nullable()
})

interface Expertise {}

export const LandingPage = () => 
{
    const value = useSelector((state: RootState) => state.landingPageReducer.value);
    const dispatch = useDispatch();
    const alert = useAlert();
    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState<Array<Expertise>>([])
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, 1500);

    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<Array<Expertise>>([]);
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, 1500);

    const [selectedLanguageValue, setSelectedLanguageValue] = useState<Array<Expertise>>([]);
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, 1500);

    const [selectedHighestDegree, setSelectedHighestDegree] = useState<Array<Expertise>>([]);
    const loadOptionsHighestDegree = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debounceHighestDegree = _.debounce(loadOptionsHighestDegree, 1500);
    
    let history = useHistory();

    return (
        <div className="center-padding-landing-page">
            <h3 className="landing_title" ><span className="title_bold">Find</span> and <span className="title_bold">book</span> the perfect author <br></br>for your <span className="title_bold">academic work</span></h3>

            <div className="submit-column-landing">

                <Formik initialValues={{ number: 1, deadline: dayjs().format('YYYY-MM-DD').toString(), budget: undefined }}
                    onSubmit={(values) => { dispatch(ActionCreators.setAuthorParams({ ...values, kindOfWork: selectedKindOfWorkValue, expertise: selectedExpertiseValue, language: selectedLanguageValue, highestDegree: selectedHighestDegree })); history.push('/search-author') }}
                    validationSchema={homeSchema}>
                    {({ dirty, isValid, handleSubmit }) => {
                        return (
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                                <div className="searchauthor-row">
                                    <div className="select-wrap landing">
                                        <label className="lbl_padd">Kind of work</label>
                                        <Field
                                            as={AsyncSelect}
                                            className="label-wrap"
                                            defaultOptions
                                            components={makeAnimated()}
                                            value={selectedKindOfWorkValue}
                                            getOptionLabel={(e: {name: string}) => e.name}
                                            getOptionValue={(e: {value: number}) => e.value}
                                            loadOptions={debouncedLoadOptions}
                                            onChange={(value: Array<Expertise>) => setKindOfWorkSelectedValue(value)}
                                            name="kindOfWorkIds">
                                        </Field>
                                    </div>
                                    <div className="landing-form-devider"></div>
                                    <div className="select-wrap landing in">
                                        <label className="lbl_padd">Area of expertise</label>
                                        <Field
                                            as={AsyncSelect}
                                            defaultOptions
                                            className="label-wrap"
                                            components={makeAnimated()}
                                            value={selectedExpertiseValue}
                                            getOptionLabel={(e: {name: string}) => e.name}
                                            getOptionValue={(e: {value: number}) => e.value}
                                            loadOptions={debouncedExpertiseOptions}
                                            onChange={(value: Array<Expertise>) => setExpertiseSelectedValue(value)}
                                            name="expertiseAreaIds">
                                        </Field>
                                    </div>
                                    <div className="landing-form-devider"></div>
                                    <div className="select-wrap landing in">
                                        <label className="lbl_padd">Language</label>
                                        <Field
                                            as={AsyncSelect}
                                            defaultOptions
                                            className="label-wrap"
                                            components={makeAnimated()}
                                            value={selectedLanguageValue}
                                            getOptionLabel={(e: {name: string}) => e.name}
                                            getOptionValue={(e: {value: number}) => e.value}
                                            loadOptions={debouncedLanguageOptions}
                                            onChange={(value: Array<Expertise>) => setSelectedLanguageValue(value)}
                                            name="languageIds">
                                        </Field>
                                    </div>
                                    <div className="landing-form-devider"></div>
                                    <div className="select-wrap landing in">
                                        <label className="lbl_padd">Minimum Degree</label>
                                        <Field
                                            as={AsyncSelect}
                                            defaultOptions
                                            className="label-wrap"
                                            components={makeAnimated()}
                                            value={selectedHighestDegree}
                                            getOptionLabel={(e: {name: string}) => e.name}
                                            getOptionValue={(e: {value: number}) => e.value}
                                            loadOptions={debounceHighestDegree}
                                            onChange={(value: Array<Expertise>) => setSelectedHighestDegree(value)}
                                            name="degreeIds">
                                        </Field>
                                    </div>       
                                    <div className="landing-form-devider"></div>
                                    <div className="select-wrap landing in">
                                        <label className="lbl_padd">Number of pages</label>
                                        <Field
                                            className="label-wrap landing-right"
                                            variant="outlined"
                                            name="number"
                                            label="Number"
                                            type="number"
                                            autoComplete="off">
                                        </Field>
                                    </div>
                                    <div className="landing-form-devider"></div>
                                    <div className="select-wrap landing in">
                                        <label className="lbl_padd">Deadline</label>
                                        <Field
                                            className="label-wrap landing-right"
                                            variant="outlined"
                                            name="deadline"
                                            label="Deadline"
                                            type="date"
                                            autoComplete="off">
                                        </Field>
                                    </div>
                                    <div className="landing-form-devider"></div>
                                    <div className="select-wrap landing in">
                                        <label className="lbl_padd">Price per page </label>
                                        <Field
                                            className="label-wrap landing-right"
                                            variant="outlined"
                                            name="budget"
                                            label="Budget"
                                            type="number"
                                            placeholder="Optional"
                                            autoComplete="off">
                                            
                                        </Field>
                                    </div>
                                    <div className="searchauthor-column landing">
                                        <Button className="search-btn-landing" color="primary" type="submit" disabled={!dirty || !isValid} ><BiSearchAlt2 /></Button>
                                    </div>
                                </div>
                                <ErrorMessage component="p" className="field-colorchange ml-3" name="number" />
                                <ErrorMessage component="p" className="field-colorchange ml-3" name="budget" />
                            </Form>)
                    }}
                </Formik>
            </div>

            { value ? <HowDoesThisWork /> : ''}
            
            <CustomerAuthorLandingInfo/>
            
        </div>

    );
}

export default LandingPage;