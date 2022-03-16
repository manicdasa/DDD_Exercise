import dayjs from 'dayjs';
import React from 'react';
import { AiOutlineClose } from 'react-icons/ai';
import { GoEyeClosed } from 'react-icons/go';
import { HiCheck } from 'react-icons/hi';
import { ImCross } from 'react-icons/im';
import { Link } from 'react-router-dom';
import { Badge, Button, NavLink } from 'reactstrap';
import { ProcessCustomerExpertiseArea, ProcessCustomerKindOfWork } from '../../services/AdminServices';

export const customStyles = {
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

export const activeAuthorsColumns = [
    {
        name: 'First Name',
        selector: 'firstName',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.firstName}</div>,
    },
    {
        name: 'Last Name',
        selector: 'lastName',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastName}</div>,
    },
    {
        name: 'Username',
        selector: 'username',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.username}</NavLink></div></div>,
      },
      {
        name: 'Registered',
        selector: 'dateRegistered',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(projects.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active ',
        selector: 'noActiveProjects',
          sortable: true,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed ',
        selector: 'noClosedProjects',
          sortable: true,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noClosedProjects}</div></div>,
      },
      {
        name: 'Created',
        selector: 'noProjectsCreated',
          sortable: false,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noProjectsCreated}</div></div>,
      },
      {
        name: 'Last Project',
        selector: 'lastProjectTitle',
          sortable: false,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastProjectTitle}</div>,
      },
      {
        name: 'Unassigned',
        selector: 'noUnassignedProjects',
          sortable: false,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.noUnassignedProjects}</div>,
      }
];

export const activeProjectsColumns = [
    {
        name: 'Project',
        selector: 'projectTopic',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to={'/dashboard/projects/id=' + `${projects.id}`}>{projects.projectTopic}</NavLink></div></div>,
      },
      {
        name: 'Status',
        selector: 'bookingStatus.Id',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3
            ? <span className="badge-text   admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='success'>Completed</Badge></span>
                                                                      : projects.bookingStatus.id === 0 
                ? <span className="badge-text   admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='danger'>Being worked on</Badge></span>
                                                                      : projects.bookingStatus.id === 2
                    ? <span className="badge-text   admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='primary'>Final version submitted</Badge> </span>
                                                                        : projects.bookingStatus.id === 1
                        ? <span className="badge-text   admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span>
                                                                      : <span className="badge-text   admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,
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
      }
];

export const activeUsersColumns = [
    {
        name: 'Username',
        selector: 'username',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.username}</NavLink></div></div>,
      },
      {
        name: 'Registered',
        selector: 'dateRegistered',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(projects.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active ',
        selector: 'noActiveProjects',
        sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed ',
        selector: 'noClosedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noClosedProjects}</div></div>,
      },
      {
        name: 'Created',
        selector: 'noProjectsCreated',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noProjectsCreated}</div></div>,
      },
      {
        name: 'Last Project ',
        selector: 'lastProjectTitle',
          sortable: false,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastProjectTitle}</div>,
      },
      {
        name: 'Unassigned ',
        selector: 'noUnassignedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.noUnassignedProjects}</div>,
      }
];

export const archivedProjectsColumns = [
    {
        name: 'Project',
        selector: 'projectTopic',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to={'/dashboard/projects/id=' + `${projects.id}`}>{projects.projectTopic}</NavLink></div></div>,
      },
      {
        name: 'Status',
        selector: 'bookingStatus.Id',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3
            ? <span className="badge-text  admin-dashboard-badge-warp" ><Badge className=" admin-dashboard-badge-warp" color='success'>Completed</Badge></span>
                                                                      : projects.bookingStatus.id === 0 
                ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className=" admin-dashboard-badge-warp" color='danger'>Being worked on</Badge></span>
                                                                      : projects.bookingStatus.id === 2
                    ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className=" admin-dashboard-badge-warp" color='primary'>Final version submitted</Badge> </span>
                                                                        : projects.bookingStatus.id === 1
                        ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className=" admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span>
                        : <span className="badge-text  admin-dashboard-badge-warp"><Badge className="badge-admin-dashboard  admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,
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
      }
];

export const authorsColumns = [
    {
        name: 'Username',
        selector: 'username',
        sortable: false,
        center: false,
        cell: (author: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{author.username}</NavLink></div></div>,
      },
      {
        name: 'First name',
        selector: 'firstName',
          sortable: false,
        center: false,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{author.firstName}</div></div>,
      },
      {
        name: 'Last name',
        selector: 'lastName',
          sortable: false,
        center: false,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{author.lastName}</div></div>,
      },
      {
        name: 'Date registered',
        selector: 'dateRegistered',
          sortable: false,
        center: false,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{dayjs(author.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active Projects',
        selector: 'noActiveProjects',
        sortable: false,
        center: true,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{author.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed Projects',
        selector: 'noClosedProjects',
        sortable: false,
        center: true,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{author.noClosedProjects}</div></div>,
      }
];

export const inactiveAuthorsColumns = [
    {
        name: 'First Name',
        selector: 'firstName',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.firstName}</div>,
    },
    {
        name: 'Last Name',
        selector: 'lastName',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastName}</div>,
    },
    {
        name: 'Username',
        selector: 'username',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.username}</NavLink></div></div>,
      },
      {
        name: 'Registered',
        selector: 'dateRegistered',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(projects.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active ',
        selector: 'noActiveProjects',
          sortable: true,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed',
        selector: 'noClosedProjects',
          sortable: true,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noClosedProjects}</div></div>,
      },
      {
        name: 'Created',
        selector: 'noProjectsCreated',
          sortable: false,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noProjectsCreated}</div></div>,
      },
      {
        name: 'Last Project',
        selector: 'lastProjectTitle',
          sortable: false,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastProjectTitle}</div>,
      },
      {
        name: 'Unassigned',
        selector: 'noUnassignedProjects',
          sortable: false,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.noUnassignedProjects}</div>,
      }
];

export const inactiveUsersColumns = [
    {
        name: 'Username',
        selector: 'username',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.username}</NavLink></div></div>,
      },
      {
        name: 'Registered',
        selector: 'dateRegistered',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(projects.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active',
        selector: 'noActiveProjects',
          sortable: true,
          center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed ',
        selector: 'noClosedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noClosedProjects}</div></div>,
      },
      {
        name: 'Created',
        selector: 'noProjectsCreated',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noProjectsCreated}</div></div>,
      },
      {
        name: 'Last Project ',
        selector: 'lastProjectTitle',
          sortable: false,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastProjectTitle}</div>,
      },
      {
        name: 'Unassigned ',
        selector: 'noUnassignedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.noUnassignedProjects}</div>,
      }
];

export const inDisputeProjectColumns = [
    {
        name: 'Project',
        selector: 'projectTopic',
        sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to={'/dashboard/projects/id=' + `${projects.id}`}>{projects.projectTopic}</NavLink></div></div>,
      },
      {
        name: 'Status',
        selector: 'bookingStatus.Id',
          sortable: true,
          center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3
            ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='success'>Completed</Badge></span>
                                                                      : projects.bookingStatus.id === 0 
                ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='danger'>Being worked on</Badge></span>
                                                                      : projects.bookingStatus.id === 2
                    ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='primary'>Final version submitted</Badge> </span>
                                                                        : projects.bookingStatus.id === 1
                          ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span>
                        : <span className="badge-text admin-dashboard-badge-warp"><Badge className="badge-text admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,
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
      }
];

export const newAuthorsColumns = [
    {
        name: 'First Name',
        selector: 'firstName',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.firstName}</div>,
    },
    {
        name: 'Last Name',
        selector: 'lastName',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastName}</div>,
    },
    {
        name: 'Username',
        selector: 'username',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.username}</NavLink></div></div>,
      },
      {
        name: 'Registered',
        selector: 'dateRegistered',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(projects.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active ',
        selector: 'noActiveProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed ',
        selector: 'noClosedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noClosedProjects}</div></div>,
      },
      {
        name: 'Created',
        selector: 'noProjectsCreated',
          sortable: false,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noProjectsCreated}</div></div>,
      },
      {
        name: 'Last Project ',
        selector: 'lastProjectTitle',
          sortable: false,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastProjectTitle}</div>,
      },
      {
        name: 'Unassigned',
        selector: 'noUnassignedProjects',
          sortable: false,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.noUnassignedProjects}</div>,
      }
];

export const newProjectsColumns = [
    {
        name: 'Project',
        selector: 'projectTopic',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to={'/dashboard/projects/id=' + `${projects.id}`}>{projects.projectTopic}</NavLink></div></div>,
      },
      {
        name: 'Status',
        selector: 'bookingStatus.Id',
        sortable: true,
        center: false,
          cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3
              ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='success'>Completed</Badge></span>
                                                                        : projects.bookingStatus.id === 0 
                  ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='danger'>Being worked on</Badge></span>
                                                                        : projects.bookingStatus.id === 2
                      ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='primary'>Final version submitted</Badge> </span>
                                                                          : projects.bookingStatus.id === 1
                          ? <span className="badge-text  admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span>
                                                                        : <span className="badge-text  admin-dashboard-badge-warp"><Badge className="  admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,
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
      }
];

export const newUsersColumns = [
    {
        name: 'Username',
        selector: 'username',
        sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{projects.username}</NavLink></div></div>,
      },
      {
        name: 'Date Registered',
        selector: 'dateRegistered',
        sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(projects.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active Projects',
        selector: 'noActiveProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed Projects',
        selector: 'noClosedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noClosedProjects}</div></div>,
      },
      {
        name: 'Projects Created',
        selector: 'noProjectsCreated',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.noProjectsCreated}</div></div>,
      },
      {
        name: 'Last Project Title',
        selector: 'lastProjectTitle',
          sortable: false,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.lastProjectTitle}</div>,
      },
      {
        name: 'Unassigned Projects',
        selector: 'noUnassignedProjects',
          sortable: true,
        center: true,
        cell: (projects: any) => <div data-tag="allowRowEvents">{projects.noUnassignedProjects}</div>,
      }
];

export const projectsColumns = [
    {
        name: 'Project',
        selector: 'projectTopic',
        sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to={'/dashboard/projects/id=' + `${projects.bookingId}`}>{projects.projectTopic}</NavLink></div></div>,
      },
      {
        name: 'Status',
        selector: 'bookingStatus.Id',
          sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{projects.bookingStatus.id === 3
            ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='success'>Completed</Badge></span>
                                                                      : projects.bookingStatus.id === 0 
                ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='danger'>Being worked on</Badge></span>
                                                                      : projects.bookingStatus.id === 2
                    ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='primary'>Final version submitted</Badge> </span>
                                                                        : projects.bookingStatus.id === 1
                        ? <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='primary'>{projects.bookingStatus.value}</Badge> </span>
                                                                      : <span className="badge-text admin-dashboard-badge-warp"><Badge className="admin-dashboard-badge-warp" color='warning'>{projects.bookingStatus.value}</Badge></span>}</div></div>,
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
        name: 'Date',
        selector: 'deadline',
          sortable: true,
        center: false,
        cell: (projects: any) => <div data-tag="allowRowEvents"><div>{dayjs(projects.deadline).format('DD.MM.YYYY').toString()}</div></div>,
      }
];

export const usersColumns = [
    {
        name: 'Username',
        selector: 'username',
        sortable: false,
        center: false,
        cell: (author: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{author.username}</NavLink></div></div>,
      },
      {
        name: 'Date registered',
        selector: 'dateRegistered',
          sortable: false,
        center: false,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{dayjs(author.dateRegistered).format('DD.MM.YYYY').toString()}</div></div>,
      },
      {
        name: 'Active Projects',
        selector: 'noActiveProjects',
        sortable: false,
        center: true,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{author.noActiveProjects}</div></div>,
      },
      {
        name: 'Closed Projects',
        selector: 'noClosedProjects',
        sortable: false,
        center: true,
        cell: (author: any) => <div data-tag="allowRowEvents"><div>{author.noClosedProjects}</div></div>,
      }
];

export const areasOfExpertiseColumns = [
    {
        name: 'Value',
        selector: 'value',
        sortable: true,
        center: false,
        cell: (expertise: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{expertise.value}</NavLink></div></div>,
      },
      {
        name: 'Description',
        selector: 'description',
        sortable: true,
        center: false,
        cell: (expertise: any) => <div data-tag="allowRowEvents"><div>{expertise.description}</div></div>,
      },
      {
        name: 'Status',
        selector: 'status',
        center: true,
        cell: (expertise: any) => <div data-tag="allowRowEvents"><div>{expertise.fieldStatusDTO.value}</div></div>,
      },
      {
        name: 'Status',
        selector: 'status',
        center: true,
        cell: (expertise: any) => <div data-tag="allowRowEvents"><div>
          { expertise.fieldStatusDTO.id === 0 ? <AiOutlineClose color="red" size="24"/> 
          : expertise.fieldStatusDTO.id === 1 ? <HiCheck color="green" size="24"/> 
          : expertise.fieldStatusDTO.id === 2 ? <GoEyeClosed color="red" size="24"/> : <div/> }</div></div>
      },
      {
        name: '',
        center: true,
        cell: (expertise: any) => <div data-tag="allowRowEvents">
          <div className="row-style">
            <Button color="primary" className="button-edits" onClick={() => { ProcessCustomerExpertiseArea(expertise.id, true, alert) }}>Aprove</Button>
            <Button color="secondary" className="button-edit" onClick={() => { ProcessCustomerExpertiseArea(expertise.id, false, alert) }}>Reject</Button>
          </div></div>
      }
];

export const kindOfWorkColumns = [
    {
        name: 'Value',
        selector: 'value',
        sortable: true,
        center: false,
        cell: (kindofwork: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{kindofwork.value}</NavLink></div></div>,
      },
      {
        name: 'Description',
        selector: 'description',
        sortable: true,
        center: false,
        cell: (kindofwork: any) => <div data-tag="allowRowEvents"><div>{kindofwork.description}</div></div>,
      },
      {
        name: 'Status',
        selector: 'status',
        center: true,
        cell: (kindofwork: any) => <div data-tag="allowRowEvents"><div>
          { kindofwork.fieldStatusDTO.id === 0 ? <AiOutlineClose color="red" size="24"></AiOutlineClose>
          : kindofwork.fieldStatusDTO.id === 1 ? <HiCheck color="green" size="24"></HiCheck>
          : kindofwork.fieldStatusDTO.id === 2 ? <GoEyeClosed color="red" size="24"></GoEyeClosed> : <div/> }</div></div>
      },
      {
        name: '',
        center: true,
        cell: (kindofwork: any) => <div data-tag="allowRowEvents">
          <div className="row-style">
            <Button color="primary" className="button-edits" onClick={() => { ProcessCustomerKindOfWork(kindofwork.id, true, alert) }}>Approve</Button>
            <Button color="secondary" className="button-edit" onClick={() => { ProcessCustomerKindOfWork(kindofwork.id, false, alert) }}>Reject</Button>
          </div></div>
      }
];

export const notificationsCenterColumns = [
    {
      name: 'Message',
      selector: 'message',
      sortable: false,
      center: false,
      cell: (notification: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash"><NavLink tag={Link} to="#">{notification.message}</NavLink></div></div>,
    },
    {
        name: 'Date Time Created',
        selector: 'message',
        sortable: false,
        center: false,
        cell: (notification: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{dayjs(notification.dateTimeCreated).format('DD.MM.YYYY').toString()}</div></div>,
    },
    {
        name: 'Seen',
        selector: 'message',
        sortable: false,
        center: false,
        cell: (notification: any) => <div data-tag="allowRowEvents"><div className="proj-title-dash">{notification.isSeen === true ?  <HiCheck size={24} color="green"/> : <ImCross size={24} color="red"/>}</div></div>,
    }
];