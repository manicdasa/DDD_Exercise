import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { ImHome } from 'react-icons/im'; 
import { Container, Card, CardBody, CardTitle } from "reactstrap";
import { FiActivity } from 'react-icons/fi';
import { HiUsers } from 'react-icons/hi';
import { IoMdMegaphone } from 'react-icons/io';
import { AiFillFile } from 'react-icons/ai';
import ReactLoading from 'react-loading';
import { useAlert } from 'react-alert';

import { GetStats } from '../../services/AdminServices';
import '../../styles/Dashboard.css';

export const Dashboard = () => 
{
    const [stats, setStats] = useState({activeProjects: 0, disputeProjects: 0, newProjects: 0, newUsers: 0})
    const [loadingValue, setLoadingValue] = useState(false);

    const history = useHistory();

    const alert = useAlert();

    useEffect(()=>
    {
        setLoadingValue(true);
        GetStats('/Admin/GetDashboardStats', alert).then((response) => { setStats(response); setLoadingValue(false); });
    },[])

    return(<div>
            { loadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                                        <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                             </div>  
                             : 
                            <div className="main-dashboard">
                                <Container fluid>
                                    <h4 className="dashboard-table-title dashProj">< ImHome className="mb-1" /> &nbsp; / Dashboard</h4>

                                    <div className="column-style">
                                        <div className="row-style">
                                    <Card className="card-row cursor mr-3" onClick={() => history.push("/dashboard/statistics/active-projects")}>
                                        <CardBody className="card-body-admin">
                                            <CardTitle className="card-title-admin" style={{ width: '' }} tag="h5"><FiActivity className="dashboard-icon admin" color="green" />&nbsp; &nbsp;  Active Projects <p className="stats-admin" style={{float: 'right'}}>{stats?.activeProjects}</p></CardTitle>
                                                </CardBody>
                                            </Card>
                                    <Card className="card-row cursor ml-3" onClick={() => history.push("/dashboard/statistics/in-dispute") }>
                                        <CardBody className="card-body-admin">
                                            <CardTitle className="card-title-admin" style={{ width: '' }} tag="h5"><IoMdMegaphone className="dashboard-icon admin" color="orange" />&nbsp; &nbsp; Projects in Dispute <p className="stats-admin" style={{ float: 'right' }}>{stats?.disputeProjects}</p></CardTitle>
                                                </CardBody>
                                            </Card>

                                        </div>

                                        <div className="row-style mt-4">
                                    <Card className="card-row  cursor mr-3" onClick={() => history.push("/dashboard/statistics/new-projects") }>
                                        <CardBody className="card-body-admin">
                                            <CardTitle className="card-title-admin" style={{ width: '' }} tag="h5"><AiFillFile className="dashboard-icon admin" color="#ff0000a1" />&nbsp; &nbsp; New Projects <p className="stats-admin" style={{ float: 'right' }}>{stats?.newProjects}</p></CardTitle>
                                                </CardBody>
                                            </Card>
                                    <Card className="card-row cursor ml-3" onClick={() => history.push("/dashboard/statistics/new-users") }>
                                        <CardBody className="card-body-admin">
                                            <CardTitle className="card-title-admin" style={{ width: '' }} tag="h5"><HiUsers className="dashboard-icon admin" color="purple" />&nbsp; &nbsp;  New Users <p className="stats-admin" style={{ float: 'right' }}>{stats?.newUsers}</p></CardTitle>
                                                </CardBody>
                                            </Card>
                                        </div>
                                    </div>
                                </Container>
                            </div>
            }
            </div>)
}

export default Dashboard;