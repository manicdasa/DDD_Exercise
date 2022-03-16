import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { AiFillCheckCircle } from 'react-icons/ai';
import { Media, NavLink } from 'reactstrap';
import Rating from '@material-ui/lab/Rating';
import { RiErrorWarningFill } from 'react-icons/ri';
import { BsDot } from 'react-icons/bs';
import { useAlert } from 'react-alert';
import '../../styles/AuthorInformations.css';
import { GetAuthorPublicInfo } from '../../services/ProfileServices';
import { BiArrowBack } from 'react-icons/bi';
import { useHistory } from 'react-router';

export const AuthorInfo = (value: any) =>
{
    const [author, setAuthor] = useState({
        id: 0,
        username: "",
        directBooking: true,
        pricePerPage: 0,
        reviewRating: 0,
        picturePath: "",
        picture: {
          id: 0,
          mimeType: "",
          seoFilename: "",
          localPath: "",
          pictureFileName: "",
          dateCreated: ""
        },
        highestDegree: 
        {
          id: 0,
          value: "",
          description: ""
        },
        expertiseAreas: 
        [
          {
            id: 0,
            value: "",
            description: ""
          }
        ],
        kindOfWorks: [
          {
            id: 0,
            value: "",
            description: ""
          }
        ],
        languages: [
          {
            id: 0,
            value: ""
          }
        ],
        avgPricePerPage: 0,
        pagesPerDay: 0,
        profileIntroduction: "",
        description: "",
        ratings: [
          {
            id: 0,
            starRating: 0,
            comment: "",
            rateWriter: 0
          }
        ]
    });

    const history = useHistory();

    const queryString = require('query-string');
    const stateParams : any = queryString.parse(window.location.pathname);

    const dispatch = useDispatch();

    const alert = useAlert();


    useEffect(()=>
    {
        if(stateParams.id !== undefined && stateParams.id !== null && stateParams.id !== 0) 
        { 
          GetAuthorPublicInfo(dispatch, stateParams.id, alert).then((res)=>setAuthor(res)); 
        }
    }, []);

    if(stateParams.id === undefined)
    {
      return <div></div>
    }

    if (author === null)
        return <div></div>;

    return(

        <div className="register-signin-box row author-details-right-cont">
            <NavLink className="back-link cursor" onClick={() => history.goBack()} >< BiArrowBack size="24" className="back-icon" /> &nbsp; Back</NavLink>
            <div className="auhor-info-details-container">
            <div className="register-signin-left-panel col-sm-12 author-img-rating-cont">
                                <div className="column">
                                    <div className="row-style">
                                    <div className="column-style center-style img-left-cont author-details-image-let"style={{height: '200px'}}>
                        
                        <Media object src={author.picturePath} className="picture-style" alt="Typewriter image"></Media>
                        <br></br>
                <Rating value={author.reviewRating} size="small" precision={0.1} readOnly/>
                <p>{author.reviewRating} of 5.0</p>
            </div>
            <div className=" user-info-tooltip author-information-main-content" style={{ width: ''}}>
                <div className="row-style user-edit-profile">
                            <h3>{author.username}</h3>              
                </div>
                  <div className="column-style mt-3">
                    <div className="row-style margin-top">
                                    <p className="pages-txt-cont">{author.directBooking ? 'Direct Booking' : 'Disabled Direct Booking '}</p>
                                    <p className="ml-1 mb-2 pages-txt-cont ">{author.directBooking ? <AiFillCheckCircle className="mb-1" color='green'/> : <RiErrorWarningFill className="mb-1" color='red'/>}</p>
                      </div>
                  </div>
                            <p className="pages-txt-cont">{author.profileIntroduction}</p>          
                                <div className="row-style auhor-info-details-details">
                    <div className="mr-5">
                                    <p className="tooltip-title pages-txt-cont">Price per page: </p>
                                    <p className="pages-txt-cont">&nbsp;{author.pricePerPage}</p>
                    </div>
                    <div className="mr-3">
                                    <p className="tooltip-title pages-txt-cont">Pages per day: </p>
                                    <p className="pages-txt-cont">&nbsp;{author.pagesPerDay}</p>
                    </div>
                </div>
                                <div className="row-style auhor-info-details-areas">
                  <div className="kow-div mr-5">
                                    <p className="tooltip-title pages-txt-cont">Kinds Of Work: </p>
                                    <p className="pages-txt-cont"> {author.kindOfWorks?.map((a: { value: any; description: any }) =>
                                     ( <li key={a.value}><BsDot/>{a.value}</li>))}   </p>
                                    
                                </div>
                            <div className="aoe-div mr-5">
                                    <p className="tooltip-title pages-txt-cont">Areas of expertise: </p>
                                    <p className="pages-txt-cont"> {author.expertiseAreas?.map((a: { value: any; description: any }) =>
                                    (<li key={a.value}><BsDot />{a.value}</li>))}  </p>
                                    
                                        </div>
                    
                    <div className="language-div">
                                    <p className="tooltip-title pages-txt-cont">Languages: </p>
                                    <p className="pages-txt-cont"> {author.languages?.map((a: { value: any; }) =>
                                    (<li key={a.value}><BsDot />{a.value}</li>))}  </p>
                                    
                    </div>
                  </div>  
                
                
              </div>
                                    </div>
                                </div>
            </div>
            </div>
                    </div>
    )
}
export default AuthorInfo;