import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
    <div>
        <h4>Welcome!</h4>
        <p>Use the navigation menu to explore different pages of this site.</p>
    </div>
);

export default connect()(Home);
