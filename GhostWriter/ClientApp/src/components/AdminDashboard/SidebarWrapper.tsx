import React, { useState } from 'react';
import { Container } from 'reactstrap';
import Sidebar from './Sidebar';
import classNames from "classnames";


export const SidebarWrapper = (props: any) =>
{
    
    const [sidebarIsOpen, setSidebarOpen] = useState(true);
    const toggleSidebar = () => setSidebarOpen(!sidebarIsOpen);

    return(
        <div className="App wrapper">
            <Sidebar toggle={toggleSidebar} isOpen={sidebarIsOpen} />
            <Container fluid className={classNames("content", { "is-open": sidebarIsOpen })} >
                {props.children}
            </Container>
        </div>
    )
}

export default SidebarWrapper;