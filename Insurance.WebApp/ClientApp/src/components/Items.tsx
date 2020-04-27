import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as ItemsStore from '../store/Items';

// At runtime, Redux will merge together...
type ItemProps =
    ItemsStore.ItemsState // ... state we've requested from the Redux store
    & typeof ItemsStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps; // ... plus incoming routing parameters


class Items extends React.PureComponent<ItemProps> {
    constructor(props: any) {
        super(props);
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeValue = this.onChangeValue.bind(this);
        this.onChangeCategoryId = this.onChangeCategoryId.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    private onChangeName(e: any) {
        var item = { ...this.props.newItem, name: e.target.value };
        this.props.updateNewItem(item);
    }

    private onChangeValue(e: any) {
        var item = { ...this.props.newItem, value: parseFloat(e.target.value ? e.target.value : 1) };
        this.props.updateNewItem(item);
    }

    private onChangeCategoryId(e: any) {
        var item = { ...this.props.newItem, categoryId: parseInt(e.target.value) };
        this.props.updateNewItem(item);
    }

    private onDelete(itemId: number) {
        var result = window.confirm("Are you sure you want to delete this item?");
        if (result)
            this.props.deleteItem(itemId);
    }

    private onSubmit(e: React.FormEvent) {
        e.preventDefault();
        this.props.addItem(this.props.newItem);
    }

    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    //// This method is called when the route parameters change
    //public componentDidUpdate() {
    //    this.ensureDataFetched();
    //}

    public render() {
        return (
            <React.Fragment>
                <h4>Manage Items</h4>
                <p>Add or delete your high-valued items here.</p>
                {this.renderItemsTable()}
            </React.Fragment >
        );
    }

    private ensureDataFetched() {
        this.props.requestCategories();
        this.props.requestItems();
    }

    private formatNumber(x: number) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    private getRows(items: ItemsStore.Item[]): ItemsStore.Item[] {
        var sortedItems = [...items];
        sortedItems.sort((a, b) => a.categoryName.localeCompare(b.categoryName));
        let rows: ItemsStore.Item[] = [];

        var prevCategory: string = '';
        var categoryRow: ItemsStore.Item = { itemId: 0, name: '', value: 0, categoryId: 0, categoryName:'' };

        sortedItems.forEach((item: ItemsStore.Item) => {
            if (item.categoryName !== prevCategory) {
                categoryRow = { categoryName: item.categoryName, value: 0, categoryId: item.categoryId, name: '', itemId: 0 };
                prevCategory = item.categoryName;
                rows.push(categoryRow);
            }

            categoryRow.value += item.value;
            rows.push(item);
        });

        return rows;
    }

    private renderItemsTable() {

        var total = 0;
        var rows = this.getRows(this.props.items);

        let options = this.props.categories.map((c) =>
            <option key={c.categoryId} value={c.categoryId}>{c.name}</option>
        );

        var isCategory: boolean = false;

        return (
            <div style={{ overflow: "auto", height: "700px" }}>
                <form onSubmit={this.onSubmit}>
                    <table className='table table-sm'>
                        <thead>
                            <tr>
                                <th>Category</th>
                                <th>Item</th>
                                <th>Value</th>
                                <th>Actions</th>
                            </tr>
                            <tr>
                                <td><select value={this.props.newItem.categoryId} onChange={this.onChangeCategoryId} className="form-control" >{options}</select></td>
                                <td><input value={this.props.newItem.name} onChange={this.onChangeName} type="text" className="form-control" required /></td>
                                <td><input value={this.props.newItem.value} onChange={this.onChangeValue} type="number" min="1" className="form-control" required /></td>
                                <td><input type="submit" value="Add" className="btn btn-primary" /></td>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                rows.map((item: ItemsStore.Item) => {

                                    isCategory = (item.itemId === 0);

                                    if (!isCategory)
                                        total += item.value;

                                    return (
                                        <tr key={item.categoryId + '-' + item.itemId}>
                                            <td>{isCategory ? item.categoryName : ''}</td>
                                            <td>{item.name}</td>
                                            <td>${this.formatNumber(item.value)}</td>
                                            <td>{!isCategory ? (<button type="button" className='btn btn-sm btn-danger' onClick={() => this.onDelete(item.itemId)}>Delete</button>) : ''}</td>
                                        </tr>
                                    );
                                })
                            }
                            <tr>
                                <th></th>
                                <th>TOTAL</th>
                                <th>${this.formatNumber(total)}</th>
                                <th></th>
                            </tr>
                        </tbody>
                    </table>
                </form>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.items, // Selects which state properties are merged into the component's props
    ItemsStore.actionCreators // Selects which action creators are merged into the component's props
)(Items as any);
