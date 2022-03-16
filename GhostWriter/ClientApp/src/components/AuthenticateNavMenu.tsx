import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Media, Navbar, NavbarBrand, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import { AiOutlineMenu } from 'react-icons/ai';
import { RiNotification3Line } from 'react-icons/ri';
import { GiWorld } from 'react-icons/gi';
import { useAlert } from 'react-alert';

import { GetCustomerInformations } from '../services/CustomerServices';
import { GetAuthorPrivateInfo } from '../services/ProfileServices';
import './AuthenticateNavMenu.css';
import type { RootState } from '../../src/store/store';

export const AuthenticateNavMenu = () => 
{
    const dispatch = useDispatch();

    const customer = useSelector((state: RootState) => state.customerReducer.customerInformations);
    const author = useSelector((state: RootState) => state.authorInformationsReducer.authorInformations);

    const loggedUserRoleAuthor = localStorage.getItem('role=Ghostwriter');    
    const loggedUserRoleCustomer = localStorage.getItem('role=Customer');

    const alert = useAlert();

    useEffect(()=>
    { 
        if(loggedUserRoleAuthor === 'Ghostwriter')
        {
            GetAuthorPrivateInfo(dispatch, alert);
        }
        else if(loggedUserRoleCustomer === 'Customer')
        {
            GetCustomerInformations(dispatch, alert);
        }
    }, []);


    return (
        <header>
            <Navbar color="white" light>
                <NavbarBrand tag={Link} to="/">
                    <h3 style={{color: 'black'}}>Website LOGO</h3>
                </NavbarBrand>
                <div className="row user-info">
                    <div className="mr-5 center">
                        <RiNotification3Line className="icon-style cursor"/>
                    </div>
                    <div className="search_author-image col-sm-2 center">
                        <Media id="img-navbar" object src={loggedUserRoleAuthor ? author.picturePath : null} className="profile_picture_style"></Media>
                    </div>
                    <div className="column mt-2">
                        <h5 style={{color: 'black'}}>{loggedUserRoleAuthor ? author.username : customer.username}</h5>
                    </div>
                    <div className="center">
                        <GiWorld className="ml-3 mt-1 icon-style cursor"/>
                    </div>
                </div>

                <UncontrolledDropdown nav inNavbar>
                    <DropdownToggle nav caret>
                        <AiOutlineMenu size={28} color='black' />
                    </DropdownToggle>
                    <DropdownMenu right>
                        <DropdownItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Logout</NavLink>
                            </NavItem>
                        </DropdownItem>
                    </DropdownMenu>
                </UncontrolledDropdown>
            </Navbar>
        </header>
    );
}

export default AuthenticateNavMenu;
