import React from 'react';
import { Navbar, NavbarBrand, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import { AiOutlineMenu } from 'react-icons/ai';
import './AuthenticateNavMenu.css';
import { stopConnection } from './Helpers/SignalRMiddleware';
import { useDispatch } from 'react-redux';

export const AdminNavMenu = () => 
{
    const dispatch = useDispatch();

    return (
        <header>
            <Navbar className="dashboard-navbar" style={{ backgroundColor: '#4e74de' }} light>
                <NavbarBrand tag={Link} to="/">
                    <h3 style={{color: 'white'}}>Ghost WRITTER - Dashboard</h3>
                </NavbarBrand>
                <div className="row">
                </div>

                  
                <UncontrolledDropdown nav inNavbar>

                    <DropdownToggle nav caret>
                        <AiOutlineMenu size={28} color='white' />
                        
                    </DropdownToggle>
                    <DropdownMenu right>
                        <DropdownItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/dashboard/login" onClick={()=> { localStorage.clear(); stopConnection(dispatch); }}>Logout</NavLink>
                            </NavItem>
                        </DropdownItem>
                    </DropdownMenu>
                </UncontrolledDropdown>
            </Navbar>
        </header>
    );
}

export default AdminNavMenu;
