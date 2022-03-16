import React, { useState }  from 'react';
import { Media, Form, Label,  Button, UncontrolledCollapse, CardBody, Card } from 'reactstrap';
import { RiSearch2Line } from 'react-icons/ri';
import { Formik, Field } from 'formik';
import { FiInfo } from "react-icons/fi";

import '../styles/CreateProject.css'

let faq = [
    {
        header: "Questions about the service",
        questions: [
            {
                question: "What is a ghostwriter?",
                answer: ` Ghostwriters are(scientific) authors who prepare texts without making any claims to authorship.
							You give your customers all rights to the texts created.Customers therefore have all rights to use the texts in any form.
							This has several advantages for both sides.The customers of ghostwriters are allowed to publish and submit the texts.
							In doing so, they will receive the fame and the proceeds from the publication.The ghostwriters, on the other hand, have a secure income for a job that you enjoy. `
				},
				{
					question: "Do the authors offer services for students?",
					answer: `Yes, our services are optimally adapted to the needs of students. Find the right ghostwriter with us.
							Our writers are experts in their field and love to write high quality papers.
							You check the quality and can hand in the work at your university.
							An automatic plagiarism check gives you additional security that there will be no problems with the delivery. `
				},
				{
					question: "Is the service also suitable for publishers?",
					answer: ` Ghostwriters offer their high quality services without putting the authorship in the foreground.
							If you are missing a paper on certain topics in the publisher's portfolio, you can buy the texts you need here.
							You discuss individually with your author which text you would like to buy. The author will write your text based on your requirements.
							After the purchase and the transfer of the payment to the author, you receive all rights to the text and can use it in your business`
				},
				{
					question: "How much does an order cost?",
					answer: `You decide how much you are willing to pay. Of course, the price per page should represent fair business to the writer.
							   A prior agreement with the author is therefore recommended`
				},
				{
					question: "Is my work unique?",
					answer: `Yes, your work will be specially written by our professional writers according to your requirements.
							In addition, all texts sold are checked by our automatic plagiarism scanner.If there are any inconsistencies, this will be`
				},
            {
                question: "Who owns the copyright to my work?",
                answer: `The authors undertake to transfer the copyright and usage rights to you after successful payment .`
            }
        ]
    },
    {
        header: "For authors",
        questions: [
            {
                question: "What advantages do I have as a ghostwriter?",
                answer: `Are you passionate about writing and want to earn money with it ? No problem! - At Studi - Autoren.de you can expect a lot of jobs from different subject areas.
                        Find your suitable jobs and work on them, let yourself be found and get in direct contact with interested customers.Customer acquisition will be much easier for you.\ N \ n
                        Payment and payment processing run smoothly through our system.
                        Since the customer shares their requirements with you right from the start, you have a direct overview of what is important when writing.
                        Let yourself be inspired by the given literature and start writing.If you have any questions, you can contact your customer directly.
                       If you have any technical questions, our support team is always available`
                },
                {
                    question: "How much do I earn on a job?",
                    answer: `You have the opportunity to negotiate with your customer.The willingness to pay is specified by the customer, but can be adjusted differently in a negotiation.`
                        },
                {
                    question: "How can I get in contact with my customers?",
                    answer: `On our site we offer you extensive chat and messaging options .`
                },
                {
                    question: "How do I get my payments?",
                    answer: `You can receive your payments via PayPal or have them transferred directly to your bank account .`
                }
            ]
                },
    {
        header: "Questions about the payment process",
            questions: [
                {
                    question: "Which payment methods are accepted?",
                    answer: `You can pay via PayPal or direct debit .`
                },
                {
                    question: "How quickly will my account be charged?",
                    answer: ``
                },
                {
                    question: "What is the recipient's name for the direct debit?",
                    answer: ``
                }
            ]
    },
]




export const FAQ = () =>
{
    const [data, setData] = useState(faq);

    const searchFAQs = (query: string) => {
        var relevantTopics = faq.filter(topic => topic.header.toLowerCase().includes(query) || topic.questions.some(question => question.question.toLowerCase().includes(query)));

        var topicsToDisplay: { header: string; questions: { question: string; answer: string; }[]; }[] = [];

        relevantTopics.forEach(topic => topicsToDisplay.push({
            header: topic.header,
            questions: topic.header.toLowerCase().includes(query) ? topic.questions : topic.questions.filter(question => question.question.toLowerCase().includes(query))
        }))

        setData(topicsToDisplay);
    }

    return(
        <div className="register-user-box row">
            <div className="register-user-left-panel col-sm-4">
                <h3>Ghost Writer Service</h3>
                <div className="left-panel-text">
                    <p>Become an Author on our platform to offer academic work for our customers.</p>
                    <p>Please fill in as much details as possible so that your profile can be dound by porential customers.</p>
                </div>
                
                    
                
                <div className="media_left_panel">
                    <Media object src="/images/FAQ/FAQ_toReplace.png" alt="Typewriter image"></Media>
                </div>
            </div>

            <div className="register-user-right-panel col-sm-8">
                <h3>FAQ</h3>
                <br></br>
                

                <Formik
                    initialValues={{ searchTerm: '' }}
                    onSubmit={(values) => alert(JSON.stringify(values))}>

                    {({ values, handleSubmit }) =>
                        <Form onSubmit={(e) => { e.preventDefault(); searchFAQs(values.searchTerm.toLowerCase()); }}>
                            <div className="help_text col-sm-12">Hello, how can we help?</div>
                            <div className="div-input-row col-sm-12 mt-3">
                                <div className="div-input-column col-sm-10">
                                    <div className="div-input-row">
                                        <span className="input-group-text mr-2"><RiSearch2Line /></span>
                                        <Field
                                            className="form-control"
                                            label="Search"
                                            type="text"
                                            name="searchTerm"
                                            autoComplete="off"
                                            placeholder="What would you like to know?"
                                            >
                                        </Field>
                                    </div>
                                </div>
                                <div className="div-input-column col-sm-2">
                                    <Button color="primary" className="btn btn-primary"type="submit">Search</Button>
                                </div>
                            </div>

                        </Form>
                    }
                </Formik>


                {data.map((topic, tIndex) =>

                    <div className="div-input-row col-sm-12 mt-3" key={"question" + tIndex}>
                        <div className="div-input-column col-sm-12">
                            <Label className="topic_header"> {topic.header}</Label>

                            {topic.questions.map((question, qIndex) =>
                                <React.Fragment
                                    key={"question" + tIndex + "_" + qIndex }>
                                    <Button className="btn btn-primary faq" id={"question" + tIndex + "_" + qIndex}>
                                        {question.question}
                                    </Button>
                                    <UncontrolledCollapse toggler={"question" + tIndex + "_" + qIndex}>
                                        <Card className="card_faq">
                                            <CardBody>
                                                {question.answer}
                                            </CardBody>
                                        </Card>
                                    </UncontrolledCollapse>
                                    <div className="line_space_btns"></div>
                                </React.Fragment>

                            )}
                           
                        </div>
                    </div>

                )}
                
            </div>
        </div>
    )
}

export default FAQ;