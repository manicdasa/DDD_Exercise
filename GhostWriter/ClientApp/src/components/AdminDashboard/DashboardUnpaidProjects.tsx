import React, { useState, useEffect } from 'react';
import DataTable from 'react-data-table-component';
import { Input, InputGroup, Button, Badge, NavLink, Form } from 'reactstrap';
import { Link } from 'react-router-dom';
import classNames from "classnames";
import { Container } from "reactstrap";
import _ from 'lodash';
import { BiSearchAlt2 } from 'react-icons/bi';
import { MdEuroSymbol } from 'react-icons/md';
import dayjs from 'dayjs';
import { AiOutlineClose, AiOutlineProject } from "react-icons/ai";
import { FaExternalLinkSquareAlt, FaExternalLinkAlt } from "react-icons/fa";



import { IoOpenOutline } from "react-icons/io5";
import { useAlert } from 'react-alert';
import ReactLoading from 'react-loading';
import { Popup } from 'react-chat-elements';
import { GetNewDataFromPageChange, SortFunction, PayAuthor, MarkAsPaidAuthor } from '../../services/AdminServices';
import NumberFormat from 'react-number-format';
import { HiCheck } from 'react-icons/hi';

const customStyles = {
    rows: {
      style: {
        minHeight: '72px', // override the row height
      }
    },
    headCells: {
      style: {
        paddingLeft: '8px', // override the cell padding for head cells
        paddingRight: '8px',
      },
    },
    cells: {
      style: {
        paddingLeft: '8px', // override the cell padding for data cells
        paddingRight: '8px',
      },
    },
};

export const DashboardUnpaidProjects = () => 
{
    const alert = useAlert();

    const [loadingState, setLoadingState] = useState(true)

    const [sidebarIsOpen, setSidebarOpen] = useState(true);
    const toggleSidebar = () => setSidebarOpen(!sidebarIsOpen);

    const [orderColumn, setOrderColumn] = useState("");

    const [sortDir, setSortDir] = useState("desc");

    const [page, setPage] = useState(0);
    const [rows, setRows] = useState(10);

    const [popup, showPopup] = useState(false);
    const [popupIban, showPopupIban] = useState(false);
    const [info, setInfo] = useState({id: 0, total: 0, paid: 0, username: '', iban: '' });
    const [paymentAmount, setPaymentAmount] = useState(0);

    const [searchParams, setSearchParams] = useState('');

    const handleChangeParams = (event : any) => 
    {
        setSearchParams(event.target.value);    
    }

    const [data, setData] = useState<any>({ totalCount: 0, items: [] });
    const [filters, setFilters] = useState<any>({field: '', sortDirection: ''})

    const Sort = (rowsF: any[], field: string, sortDirection: "desc" | "asc") =>
    {
        if(field === null)
        {
           return rowsF;
        } 
        else
        {

            if (filters.field != field || filters.sortDirection != sortDirection) {
                setOrderColumn(field);
                setSortDir(sortDirection);
            SortFunction('/Admin/GetClosedUnpaidProjects', 0, rows, searchParams, field, sortDirection, alert).then(response=> {setFilters({field, sortDirection}); setData(response);});
            return data.items;
          }
          else
            return rowsF;
        }
    }

    const MakePayment = (id: any, paymentAmount: any, authorusername: any) =>
    {
        if(paymentAmount != 0 && paymentAmount <= info.total-info.paid && paymentAmount > 0)
        {
            PayAuthor(id, paymentAmount, alert, authorusername);
        }
        else
        {
            alert.error('Payment amount must be a valid number.')
        }
        showPopup(false);
    }

    const MakePaymentIban = (id: any, paymentAmount: any, authorusername: any) =>
    {
        if(paymentAmount != 0 && paymentAmount <= info.total-info.paid && paymentAmount > 0)
        {
            MarkAsPaidAuthor(id, paymentAmount, alert, authorusername);
        }
        else
        {
            alert.error('Payment amount must be a valid number.')
        }
        showPopup(false);
    }

    let popupContent = () => 
    (<div className="switch-buttons center">
            <div className="payment-popup-container center column-style">
            <p className="txt-popup-payment">{info.username} accepts PayPal payments.</p>
                <p className="mt-1">Amount to pay: &nbsp;&#8364;&nbsp;<NumberFormat value={info.total} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                <div className="btn-cont-popup-payment">
                    <Button color="primary" className='center mt-2' onClick={() => MakePayment(info.id, info.total, info.username)}>Make Payment</Button>
                    <Button color="secondary" className='center mt-2 ml-2' onClick={() => showPopup(false) }>Cancel</Button>
                </div>
        </div>
    </div>)

    
  let popupContentIban = () => 
    (<div className="switch-buttons center">
        <div className="payment-popup-container center column-style">
            <p className="txt-popup-payment mt-1">{info.username} accepts IBAN payments.</p>
            <p className="mt-1">IBAN: {info.iban}</p>
            <p className="mt-1">Amount to pay: &nbsp;&#8364;&nbsp;<NumberFormat value={info.total} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
            <div className="btn-cont-popup-payment">
                <Button color="primary" className='center mt-2' onClick={() => MakePaymentIban(info.id, info.total, info.username)}>Mark as Paid</Button>
                <Button color="secondary" className='center mt-2 ml-2' onClick={() => showPopupIban(false) }>Cancel</Button>
              </div>
        </div>
    </div>)


    const columns = [
        {
            name: 'Project',
            selector: 'projectTopic',
            sortable: true,
            center: false,
            cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to={'/dashboard/projects/id=' + `${projects.id}`}>{projects.projectTopic}</NavLink></div></div>,
          },
          {
            name: 'Customer',
            selector: 'customerUsername',
            sortable: true,
            center: false,
            cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.customerUsername}</NavLink></div></div>,
          },
          {
            name: 'Author',
            selector: 'authorUsername',
            sortable: true,
            center: false,
            cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.authorUsername}</NavLink></div></div>,
          },
          {
            name: 'Due Date',
            selector: 'dueDate',
            sortable: true,
            center: false,
            cell: (projects: any) => <div data-tag="allowRowEvents"><div>{dayjs(projects.dueDate).format('DD.MM.YYYY').toString()}</div></div>,
          },
          {
            name: 'Date Created',
            selector: 'dateCreated',
            sortable: true,
            center: false,
            cell: (projects: any) => <div data-tag="allowRowEvents"><div>{dayjs(projects.dateCreated).format('DD.MM.YYYY').toString()}</div></div>,
          },
          {
              name: 'Author payment',
              selector: 'amountPaid',
              sortable: true,
              center: true,
              cell: (projects: any) => <div style={{  }} data-tag="allowRowEvents"><div><NumberFormat value={projects.amountPaid} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} />&#8364;/</div><div><NumberFormat value={projects.totalAmountToPay} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} />&#8364;</div></div>,
        },
        {
            name: 'Payment Fully Done',
            selector: 'isCompletelyPaid',
            sortable: true,
            center: true,
            cell: (projects: any) => <div data-tag="allowRowEvents"><div> {projects.isCompletelyPaid ? <HiCheck color="green" size="24"></HiCheck> : <AiOutlineClose color="red" size="24"></AiOutlineClose>}</div></div>,
            /*                ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='success'>{projects.bookingStatus.value}</Badge></span>*/
            /*                                        : <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,*/
        },
          //{
          //  name: 'Total amount for Author',
          //  selector: 'totalAmountToPay',
          //    sortable: true,
          //  center: true,
          //  cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.totalAmountToPay}</div></div>,
          //},
          {
            name: 'Customer Refund',
            selector: 'customerRefund',
            sortable: true,
            center: true,
                          cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3 ? '/' : <div><NumberFormat value={projects.customerRefund} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} />&#8364;</div>}</div></div>,
          },
          {
            name: 'Status',
            selector: 'bookingStatus.Id',
            sortable: true,
            center: true,
              cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3
                  ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='success'>{projects.bookingStatus.value}</Badge></span>
                                                                          : projects.bookingStatus.id === 0 
                      ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='danger'>Being worked on</Badge></span>
                                                                          : projects.bookingStatus.id === 2
                          ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span>
                                                                            : projects.bookingStatus.id === 1
                              ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span> 
                                                                              : projects.bookingStatus.id === 4                     
                                  ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge> </span>
                                                                                : projects.bookingStatus.id === 1
                                      ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge> </span>
                                                                                  : projects.bookingStatus.id === 6
                                          ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge> </span>
                                          : <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,
          },
          {
            name: 'Action',
            selector: '',
            sortable: false,
            center: true,
            cell: (projects: any) => 
              <div className="" data-tag="allowRowEvents">
                  <Button className="make-payment-btn" color="" 
                    onClick={() => 
                    { 
                      if(projects.iban === null)
                      {
                        setInfo({ id: projects.id, total: projects.totalAmountToPay, paid: projects.amountPaid, username: projects.authorUsername, iban: '' }); 
                        showPopup(true); 
                      }
                      else
                      {
                        setInfo({ id: projects.id, total: projects.totalAmountToPay, paid: projects.amountPaid, username: projects.authorUsername, iban: projects.iban }); 
                        showPopupIban(true); 
                              }
                        }}><FaExternalLinkAlt size="12" />&nbsp;&nbsp;Payment</Button></div>,
        },
        
    ];

    const changeRowPerPage = (perPage: any)=> 
    {
        setRows(perPage);
        GetNewDataFromPageChange('/Admin/GetClosedUnpaidProjects', page, perPage, searchParams, orderColumn, sortDir, alert).then((response) => setData(response));
    }

    const changePage = (state: any) => 
    {
        setPage(state-1);
        GetNewDataFromPageChange('/Admin/GetClosedUnpaidProjects', state - 1, rows, searchParams, orderColumn, sortDir, alert).then((response) => setData(response));
    }
    useEffect(() =>
    {
        setLoadingState(true);
        SortFunction('/Admin/GetClosedUnpaidProjects', page, rows, searchParams, "", 'desc', alert).then(response=> { setData(response); setLoadingState(false); });
    }, [])

    return(
        <div className="App wrapper">
            <div className="main-dashboard">
                <Container fluid className={classNames("content", { "is-open": sidebarIsOpen })} >
                    <h4 className="dashboard-table-title dashProj">< AiOutlineProject className="icon-paymnet-dash" /> &nbsp; Dashboard Closed Unpaid Projects</h4>
            <div className="row-style search">
                        <Form className="row-style" onSubmit={(e) => { e.preventDefault(); setLoadingState(true); GetNewDataFromPageChange('/Admin/GetClosedUnpaidProjects', 0, rows, searchParams, orderColumn, sortDir, alert).then((response) => { setData(response); setLoadingState(false); })}}>
            <InputGroup>
              <Input type="text" className="input-search-proj" placeholder="Search projects" value={searchParams} onChange={handleChangeParams} />
            </InputGroup>
            <Button color="primary" type="submit"><BiSearchAlt2 /></Button>
            </Form>
            </div>
            <br/>            
            { loadingState ? <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                <ReactLoading className="mt-5" type='bars' color='#0080FF' />
            </div> :
            <DataTable
                keyField={'id'}
                data={data.items}
                columns={columns}
                noHeader
                striped
                highlightOnHover
                sortFunction={(rows: any[], field: string, sortDirection: "desc" | "asc") => { return Sort(rows, field, sortDirection) as any[]; }}
                pointerOnHover
                pagination 
                paginationServer
                paginationResetDefaultPage
                paginationRowsPerPageOptions={[5,10,15,20,25,50,100]}
                paginationTotalRows={data.totalCount}
                onChangePage={changePage}
                onChangeRowsPerPage={changeRowPerPage}
                customStyles={customStyles}
                responsive={true}
                className="table-width dashboard"/> }
                </Container>
                </div>
                    <Popup show={popup} header='PayPal Payment' headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopup(false); }}]}
                        renderContent={() => { return popupContent() }}/>  
                      <Popup show={popupIban} header='IBAN Payment' headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopupIban(false); }}]}
                          renderContent={() => { return popupContentIban() }}/>   
        </div>)
}

export default DashboardUnpaidProjects;