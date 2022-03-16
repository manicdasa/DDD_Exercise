import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';

export const RedirectComponent = () =>
{
    const history = useHistory();
    
    useEffect(() => { history.goBack(); }, []);
    return(<div className="row-style">
        <div className="register-user-box row">
            <div className="register-signin-left-panel col-sm-9 project-content">
            </div></div></div>)
}

export default RedirectComponent;


