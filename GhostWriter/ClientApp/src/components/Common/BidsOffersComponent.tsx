import React, { useState, useEffect } from 'react';
import { Col } from 'reactstrap';
import _ from 'lodash';
import ReactPaginate from 'react-paginate';
import ReactLoading from 'react-loading';
import { useAlert } from 'react-alert';

import { GetProposalInfo } from '../../services/ProposalServices';

export const BidsOffersComponent = ({ array, totalCount, setArray, setTotalCount, PER_PAGE, baseUrl, noProjectsDisplay, projectsMapFunction }: any) => 
{
    //the current page
    const [currentPage, setCurrentPage] = useState<number>(0);

    //data recieved from server
    const [loading, setLoading] = useState<boolean>(false);

    //calculated data
    const pageCount = Math.ceil(totalCount / PER_PAGE);
    const offset = currentPage * PER_PAGE;

    const alert = useAlert();

    const refreshFunction = () => {
        setLoading(true);

        GetProposalInfo(baseUrl, alert).then((data) => 
        {
            setArray(data?.items ?? []);
            setTotalCount(data?.totalCount ?? 0);
            setLoading(false);
        })
    }

    const currentPageData = array?.slice(offset, offset + PER_PAGE).map((value: any) => projectsMapFunction(value, refreshFunction) ); 




    useEffect(() => 
    {
        refreshFunction();
    }, [])

    return (
        <Col sm="12">
            {loading ?
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                    <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF' />
                </div> :
                <div>
                    {totalCount === 0 ? noProjectsDisplay : currentPageData}
                    {totalCount === 0 ? <div /> : <div><div className="commentBox center">
                        <ReactPaginate
                            previousLabel={"← Previous"}
                            nextLabel={"Next →"}
                            pageRangeDisplayed={5}
                            marginPagesDisplayed={5}
                            pageCount={pageCount}
                            onPageChange={(value) => 
                            {
                                setCurrentPage(value.selected);
                            }}
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
                        <p className="page-count-sidebar1">Page {currentPage + 1} of {pageCount === 0 ? 1 : pageCount}</p></div>}
                </div>}
        </Col>
    );
}

export default BidsOffersComponent;