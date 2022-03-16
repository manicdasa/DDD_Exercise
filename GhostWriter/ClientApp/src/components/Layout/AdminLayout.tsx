import * as React from 'react';
import { AdminNavMenu } from '../AdminNavMenu';
import "./AdminLayout.css"

export default class AdminLayout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return (
            <div>
                
                <React.Fragment>
                    
                    <AdminNavMenu />
                    {this.props.children}
                </React.Fragment>

            </div>
        );
    }
}