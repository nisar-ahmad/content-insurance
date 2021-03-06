import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { MemoryRouter } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import App from './App';
import configureStore from './store/configureStore';

it('renders without crashing', () => {
    // Create browser history to use in the Redux store
    const history = createBrowserHistory();

    // Get the application-wide store instance, prepopulating with state from the server where available.
    const store = configureStore(history);

    ReactDOM.render(
        <Provider store={store}>
            <MemoryRouter>
                <App/>
            </MemoryRouter>
        </Provider>, document.createElement('div'));
});
