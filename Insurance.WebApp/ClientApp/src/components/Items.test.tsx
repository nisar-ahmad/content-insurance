import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { MemoryRouter } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import Items from './Items';
import configureStore from '../store/configureStore';

it('Item component renders title correctly', () => {
    // Create browser history to use in the Redux store
    const history = createBrowserHistory();

    // Get the application-wide store instance, prepopulating with state from the server where available.
    const store = configureStore(history);

    ReactDOM.render(
        <Provider store={store}>
            <MemoryRouter>
                <Items />
            </MemoryRouter>
        </Provider>, document.createElement('div'));

    var titleLabel = document.getElementsByName('h4');
    expect(titleLabel?.innerHTML).not.toBeNull(); 
});
