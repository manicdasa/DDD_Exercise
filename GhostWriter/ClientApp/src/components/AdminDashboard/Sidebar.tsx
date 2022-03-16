import React from "react";
import { Nav, NavLink } from "reactstrap";
import { Link } from 'react-router-dom';
import classNames from "classnames";
import SubMenu from './SubMenu';

import { ImHome } from 'react-icons/im'; 
import { FaHome } from 'react-icons/fa';
import { MdDashboard, MdNotificationsActive } from "react-icons/md";
import { IoStatsChartSharp } from "react-icons/io5";
import { RiUserSettingsLine } from "react-icons/ri";
import { FiAlignCenter } from 'react-icons/fi';

const SideBar = ({ isOpen, toggle }: any) => (

  <div className={classNames("sidebar admin", { "is-open": isOpen })}>
        
        <div className="sidebar-header close">
      <span color="info" onClick={toggle} style={{ color: "#fff" }}>
        &times;
      </span>
      <br/>
    </div>  
    <div className="side-menu">
            <Nav vertical className="list-unstyled pb-3">
                <div className="submenu-div  cursor">< ImHome className="submenu-icon" size="20" /><NavLink tag={Link} to="/dashboard" className="dropdown-toggle cursor">Home</NavLink></div>
                <div className="submenu-div  cursor" >< MdDashboard className="submenu-icon" size="20" /> <SubMenu title="Dashboard" icon={MdDashboard} items={submenus[0]} /></div>
                <div className="submenu-div  cursor">< IoStatsChartSharp className="submenu-icon" size="20" /><SubMenu title="Project Statistics" icon={FaHome} items={submenus[1]} /></div>
                <div className="submenu-div  cursor">< RiUserSettingsLine className="submenu-icon" size="20" /><SubMenu title="Users Statistics" icon={FaHome} items={submenus[2]} /></div>
                <div className="submenu-div  cursor">< RiUserSettingsLine className="submenu-icon" size="20" /><SubMenu title="Authors Statistics" icon={FaHome} items={submenus[3]} /></div>
                <div className="submenu-div  cursor">< FiAlignCenter className="submenu-icon" size="20" /><SubMenu title="Lookups" icon={FaHome} items={submenus[4]} /></div>
                <div className="submenu-div  cursor">< MdNotificationsActive className="submenu-icon" size="20" /><SubMenu title="Notifications" icon={MdNotificationsActive} items={submenus[5]} /></div>
      </Nav>
    </div>
  </div>
);

const submenus = [
    [
      {
        title: "Users",
        target: "/dashboard/users",
      },
      {
        title: "Authors",
        target: "/dashboard/authors",
      },
      {
        title: "Projects",
        target: "/dashboard/projects",
      }
    ],
    [
      {
        title: "New Projects",
        target: "/dashboard/statistics/new-projects",
      },
      {
        title: "Active Projects",
        target: "/dashboard/statistics/active-projects",
      },
      {
        title: "In Dispute",
        target: "/dashboard/statistics/in-dispute",
      },
      {
        title: "Archived Projects",
        target: "/dashboard/statistics/archived-projects",
      },
      {
        title: "Unpaid Projects",
        target: "/dashboard/statistics/unpaid-projects",
      }
    ],
    [
        {
            title: 'New Users',
            target: '/dashboard/statistics/new-users'
        },
        {
            title: 'User Activity',
            target: '/dashboard/statistics/user-activity'
        },
        {
            title: 'Inactive Users',
            target: '/dashboard/statistics/inactive-users'
        }
    ],
    [
        {
            title: 'New Authors',
            target: '/dashboard/statistics/new-authors'
        },
        {
            title: 'Author Activity',
            target: '/dashboard/statistics/author-activity'
        },
        {
            title: 'Inactive Authors',
            target: '/dashboard/statistics/inactive-authors'
        }
    ],
    [
        {
            title: 'Kinds Of Work',
            target: '/dashboard/kinds-of-work'
        },
        {
            title: 'Areas Of Expertise',
            target: '/dashboard/areas-of-expertise'
        }
    ],
    [
        {
            title: 'Notifications Center',
            target: '/dashboard/notifications'
        }
    ]
  ];
  

export default SideBar;
