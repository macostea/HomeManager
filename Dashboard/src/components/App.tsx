import React from 'react';
import { Switch, Route, Redirect, withRouter } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import PropTypes from 'prop-types';

import LayoutComponent from '../components/Layout/Layout';

const App = () => (
    <Switch>
        <Route path="/" exact render={() => <Redirect to="/app" />} />
        <Route path="/app" exact component={LayoutComponent} />
    </Switch>
)

export default withRouter(App);