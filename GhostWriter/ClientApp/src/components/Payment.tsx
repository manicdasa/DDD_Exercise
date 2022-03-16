import React, { useState, useEffect }  from 'react';
import { Media } from 'reactstrap';
import '../styles/CreateProject.css'
import { useHistory } from 'react-router-dom';
import { FaReceipt } from "react-icons/fa";
import ReactLoading from 'react-loading';
import { GeneratePaymentToken, MakePayment } from '../services/PaymentService';
import NumberFormat from 'react-number-format';
import { useAlert } from 'react-alert';
import { ErrorHandler } from '../services/ErrorServices';
var dropin = require('braintree-web-drop-in');

export const Payment = (props: any) =>
{
    const [braintreeInstance, setBraintreeInstance] = useState<any>(null);
    const [loadingValue, setloadingValue] = useState<boolean>(false);
    const alert = useAlert();
    const [paymentCompleted, setpaymentCompleted] = useState<boolean>(false);

    const [payment, setPayment] = useState({ id: 0, projectTopic: '', pagesNo: 0, totalPrice: 0, headProposalId: 0, financialOffer: 0 });

    const history = useHistory();  

    useEffect(() => {
        if (props.location.state === undefined && props.location.paymentDetails === undefined) {
            history.push('/customer-profile');
        }
        else {
            if(props.location.state !== undefined)
            {
                setPayment(props.location.state.paymentDetails);
            }
            else
            {
                setPayment(props.location.paymentDetails);
            }
        }
    }, [payment])   


    return (
        <div className="register-user-box row">
            {/* <div className="register-user-left-panel col-sm-4">
                <h3>Ghost Writer Payment Page</h3>
                <div className="left-panel-text">
                    <p>Become an Author on our platform to offer academic work for our customers.</p>
                    <p>Please fill in as much details as possible so that your profile can be dound by porential customers.</p>
                </div>                                
                
                <div className="media_left_panel">
                    <Media object src="/images/Login/payment.png" alt="Typewriter image"></Media>
                </div>
            </div>*/}

            <div className="register-user-right-panel col-sm-12">
                <h3>Payment</h3>
                <br></br>

                <div className="payment-proj-details-title"> Project Details <div className="title-icon"> <FaReceipt /></div></div>
                <div className="booking-details col-sm-12">
                    
                    <div className="project col-sm-12">
                        <div className="lbls payment title"> Project name </div> <div className="badge-text booking-status payment"><span className="badge badge-info">Being worked on. Payment not made yet</span></div>
                        <input className="col-sm-12 project-title-pay" type="text" value={ payment.projectTopic } name="projectName" disabled={true} /> 
                    </div>
                    <div className="price col-sm-12">

                    <div className="page-numbers col-sm-4"> 
                            <div className="lbls payment"> Number of pages </div>
                            <input className="input-page-numbers" type="number" name="numberOfPages" value={ payment.pagesNo } disabled={true} /> <span className="tim-s">x</span>
                    </div>                   

                    <div className="price-page col-sm-4">
                            <div className="lbls payment"> Price per page </div>
                            <div className="payment-format-number">&#8364;&nbsp;<NumberFormat value={payment.totalPrice !== undefined ? payment.totalPrice / payment.pagesNo : payment.financialOffer / payment.pagesNo} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /><span className="plus-s">=</span></div>                           
                    </div>

                    <div className="total col-sm-4">
                            <div className="lbls payment"> Total </div>
                            <div className="payment-format-number total">&#8364;&nbsp;<NumberFormat value={payment.totalPrice !== undefined ? payment.totalPrice : payment.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></div>                           
                    </div>
                    </div>
                </div>
                <div className="dvd"></div>
                {braintreeInstance === null &&
                    <div className="btn col-sm-12">
                    <button type="button"
                        className="btn btn-primary btn-lg"
                        onClick={() => {

                            setloadingValue(true);
                            GeneratePaymentToken(alert).then((token) => {
                                dropin.create({
                                    authorization: token,
                                    container: '#dropin-container'
                                }).then((dropinInstance: any) => { setBraintreeInstance(dropinInstance); })
                            })
                        }}>
                        Pay with a credit card
                </button>
                    </div>
                }

                <div>
                    {loadingValue && braintreeInstance === null  ? <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                         {/* type za react-loading: blank balls bars bubbles cubes cylon spin spinningBubblesspokes */}
                                         {/* https://www.npmjs.com/package/react-loading */}
                                         <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF' />
                                   </div>
                                   : <div> </div>}
                    {paymentCompleted === false &&

                        <div>

                    <div id="dropin-container"></div>

                    {braintreeInstance != null && paymentCompleted === false &&
                    <div className="btn-pay col-sm-12">
                        <button type="button"
                            className="btn btn-primary btn-lg"
                            onClick={() => {
                                braintreeInstance.requestPaymentMethod().then((payload: any) => { MakePayment(payment.headProposalId, payload.nonce, alert).then((response) => 
                                    { 
                                        if(response.success === false) 
                                        { 
                                            ErrorHandler(response, "Error occurred!", alert);
                                        } 
                                        else if(response.success === true) 
                                        { 
                                            alert.success('Your payment was successful.'); 
                                            setpaymentCompleted(true);
                                        }
                                        } )});
                            }}>
                        Complete payment
                        </button>
                    </div>
                            }
                        </div>}

                    {paymentCompleted ?
                        <div>
                            <div className="payment-successful-frontPage" onClick={() => history.goBack() } >Return to project </div>
                        </div> : ''}

                </div>

                
            </div>
        </div>
    )
}

export default Payment;