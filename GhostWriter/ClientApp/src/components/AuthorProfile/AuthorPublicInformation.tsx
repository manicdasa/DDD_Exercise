import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { AiFillCheckCircle } from 'react-icons/ai';
import { Media } from 'reactstrap';
import Rating from '@material-ui/lab/Rating';
import { RiErrorWarningFill } from 'react-icons/ri';
import { BsDot } from 'react-icons/bs';
import { useAlert } from 'react-alert';

import '../../styles/AuthorInformations.css';
import { GetAuthorPublicInfo } from '../../services/ProfileServices';

export const AuthorPublicInformation = (value: any) =>
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

    const dispatch = useDispatch();

    const alert = useAlert();

    useEffect(()=>
    {
        if(value.value !== undefined && value.value !== null && value.value !== 0) 
        { 
          GetAuthorPublicInfo(dispatch, value.value, alert).then((res)=>setAuthor(res)); 
        }
    }, []);

    if(value.value === undefined)
    {
      return <div></div>
    }

    if (author === null)
        return <div></div>;

    return(
                <div className="mt-5 " style={{height: '', width: ''}}>  
            <div className="register-user-box row tooltip-cont">
                                <div className="column">
                                    <div className="row-style">
                                    <div className="column-style center-style img-left-cont"style={{height: '200px'}}>
                        <Media object src={author.picturePath} className="picture-style" alt="Typewriter image"></Media>
                        <br></br>
                <Rating value={author.reviewRating} size="small" precision={0.1} readOnly/>
                <p>{author.reviewRating} of 5.0</p>
            </div>
            <div className=" user-info-tooltip" style={{ width: ''}}>
                <div className="row-style user-edit-profile">
                            <h3>{author.username}</h3>                        
                </div>
                        <div className="column-style mt-3">
                    <div className="row-style margin-top">
                        <p className="">{author.directBooking ? 'Direct Booking' : 'Disabled Direct Booking '}</p>
                        <p className="ml-1 mb-2">{author.directBooking ? <AiFillCheckCircle className="mb-1" color='green'/> : <RiErrorWarningFill className="mb-1" color='red'/>}</p>
                                </div>
                                
                            </div>
                            <div className="icon-top-right"><Media object src="/images/author.svg" width="30" color="#212529" alt="author icon"></Media></div>

                            
                    
                    <div className="kow-div">
                                <p className="tooltip-title">Kinds Of Work: </p>
                      {author.kindOfWorks?.map((a: { value: any; description: any }) =>
                                     ( <li key={a.value}><BsDot/>{a.value}</li>))}  
                                    
                                </div>
                            <div className="aoe-div">
                                <p className="tooltip-title">Areas of expertise: </p>
                                {author.expertiseAreas?.map((a: { value: any; description: any }) =>
                                    (<li key={a.value}><BsDot />{a.value}</li>))}  
                                    
                                        </div>
                    
                    <div className="language-div">
                                <p className="tooltip-title">Languages: </p>
                                {author.languages?.map((a: { value: any; }) =>
                                    (<li key={a.value}><BsDot />{a.value}</li>))}  
                                    
                    </div>
                    
                
                
              </div>
                                    </div>
                                </div>
                         </div>
                    </div>
    )
}
export default AuthorPublicInformation;