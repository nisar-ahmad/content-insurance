import { reducer, actionCreators, unloadedState, Item, KnownAction } from './Items';

it('reducer should initialize with unloaded state', () => {

    var item = { itemId: 2, categoryId: 2, name: 'Updated' }
    expect(reducer(undefined, {})).toEqual(unloadedState);
});

it('reducer should handle UpdateNewItemAction', () => {

    var item: Item = { itemId: 2, categoryId: 2, name: 'Item 2', categoryName: 'Category 2', value:2 }
    var newState = { ...unloadedState };
    newState.newItem = item;

    expect(reducer(unloadedState, { type: 'UPDATE_NEW_ITEM', item: item })).toEqual(newState);
});

it('action creater should dispatch update item action', () => {

    var item: Item = { itemId: 2, categoryId: 2, name: 'Item 2', categoryName: 'Category 2', value: 2 }
    expect(actionCreators.updateNewItem(item)).not.toBeNull();
});
