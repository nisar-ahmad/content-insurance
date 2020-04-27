import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { webAPIUrl } from '../AppSettings'

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface ItemsState {
    isLoading: boolean;
    items: Item[];
    categories: Category[];
    newItem: Item;
}

export interface Category {
    categoryId: number;
    name: string;
}

export interface Item {
    itemId: number;
    name: string;
    value: number;
    categoryId: number;
    categoryName: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface GetItemsAction {
    type: 'REQUEST_ITEMS';
}

interface ReceiveItemsAction {
    type: 'RECEIVE_ITEMS';
    items: Item[];
}

interface DeleteItemAction {
    type: 'DELETE_ITEM';
    itemId: number;
}

interface AddItemAction {
    type: 'ADD_ITEM';
    item: Item;
}

interface GetCategoriesAction {
    type: 'REQUEST_CATEGORIES';
}

interface ReceiveCategoriesAction {
    type: 'RECEIVE_CATEGORIES';
    categories: Category[];
}

interface UpdateNewItemAction {
    type: 'UPDATE_NEW_ITEM';
    item: Item;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
export type KnownAction = GetItemsAction | ReceiveItemsAction | AddItemAction | DeleteItemAction | GetCategoriesAction | ReceiveCategoriesAction | UpdateNewItemAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestItems: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.items) {
            fetch(`${webAPIUrl}/item`)
                .then(response => response.json() as Promise<Item[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ITEMS', items: data });
                })
                .catch(error => console.log(error));

            dispatch({ type: 'REQUEST_ITEMS' });
        }
    },
    addItem: (item: Item): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.items) {
            fetch(`${webAPIUrl}/item`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            })
                .then(response => response.json())
                .then(data => {
                    dispatch({ type: 'ADD_ITEM', item: data });
                })
                .catch(error => console.log(error));
        }
    },
    updateNewItem: (item: Item): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.items)
            dispatch({ type: 'UPDATE_NEW_ITEM', item: item });
    },
    deleteItem: (itemId: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.items) {
            fetch(`${webAPIUrl}/item/${itemId}`, { method: 'DELETE' })
                .then(response => response.json() as Promise<Item>)
                .then(data => {
                    dispatch({ type: 'DELETE_ITEM', itemId: itemId });
                })
                .catch(error => console.log(error));
        }
    },
    requestCategories: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.items) {
            fetch(`${webAPIUrl}/category`)
                .then(response => response.json() as Promise<Category[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_CATEGORIES', categories: data });
                })
                .catch(error => console.log(error));

            dispatch({ type: 'REQUEST_CATEGORIES' });
        }
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const unloadedState: ItemsState = {
    items: [], categories: [], newItem: { itemId: 0, categoryId: 1, name: '', value: 1, categoryName: '' }, isLoading: false
};

export const reducer: Reducer<ItemsState> = (state: ItemsState | undefined, incomingAction: Action): ItemsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;

    switch (action.type) {
        case 'REQUEST_ITEMS':
        case 'REQUEST_CATEGORIES':
            return {
                ...state,
                isLoading: true
            };
        case 'RECEIVE_ITEMS':
            return {
                ...state,
                items: action.items,
                isLoading: false
            };
        case 'DELETE_ITEM':
            return {
                ...state,
                items: state.items.filter(i => i.itemId !== action.itemId), // remove item
            };
        case 'ADD_ITEM':
            var items = [...state.items];
            items.push(action.item);
            return { ...state, items: items, newItem: unloadedState.newItem };

        case 'UPDATE_NEW_ITEM':
            return {
                ...state,
                newItem: action.item
            }
        case 'RECEIVE_CATEGORIES':
            return {
                ...state,
                categories: action.categories,
                isLoading: false
            };
        default:
            return state;
    }
};
