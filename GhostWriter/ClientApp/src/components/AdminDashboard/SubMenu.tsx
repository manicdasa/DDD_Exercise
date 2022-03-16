import React, { useState } from "react";
import classNames from "classnames";
import { Collapse, NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import '../../styles/Sidebar.css'

const SubMenu = (props: any) => {
  const [collapsed, setCollapsed] = useState(true);
  const toggle = () => setCollapsed(!collapsed);
  
  const { icon, title, items } = props;

  return (
    <div>
      <NavItem
        onClick={toggle}
        className={classNames({ "menu-open": !collapsed })}
      >
        <NavLink className="dropdown-toggle">
          {title}
        </NavLink>
      </NavItem>
      <Collapse
        isOpen={!collapsed}
        navbar
        className={classNames("items-menu", { "mb-1": !collapsed })}
      >
        {items.map((item:any, index:any) => (
          <NavItem key={index} className="pl-4 sidebar-submenu-item">
            <NavLink tag={Link} to={item.target}>
              {item.title}
            </NavLink>
          </NavItem>
        ))}
      </Collapse>
    </div>
  );
};

export default SubMenu;
