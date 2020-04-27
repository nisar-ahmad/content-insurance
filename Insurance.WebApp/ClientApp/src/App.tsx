import * as React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Items from './components/Items';
import { NotFoundPage } from './components/NotFoundPage'

import './custom.css'

export default () => (
    <Layout>
        <Switch>
            <Route exact path='/' component={Items} />
            <Route path='/items' component={Items} />
            <Route path='/home' component={Home} />
            <Route component={NotFoundPage} />
        </Switch>
    </Layout>
);
