import React, { ReactNode } from 'react';
import { Switch, Route, withRouter } from 'react-router';

import Dashboard from '../../pages/dashboard/Dashboard';

import './Layout.scss';

class LayoutComponent extends React.Component<any, any> {
    render() {
        return (
            <main className="content">
                <Switch>
                    <Route path="/app" exact component={Dashboard} />
                </Switch>
            </main>
        );
    }
}

export default withRouter(LayoutComponent);
