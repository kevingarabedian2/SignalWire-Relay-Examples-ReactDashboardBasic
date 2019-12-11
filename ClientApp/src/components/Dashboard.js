import React, { Component } from 'react';

export class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props);
        this.state = { realtimedata: [], loading: true };

        fetch('api/Dashboard')
            .then(response => response.json())
            .then(data => {
                this.setState({ realtimedata: data, loading: false });
            });
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    componentDidMount() {
        this.interval = setInterval(() => {
            fetch('api/Dashboard')
                .then(response => response.json())
                .then(data => {
                    this.setState({ realtimedata: data, loading: false });
                });
        }, 2000);
    }

    static renderDashboard(info) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Date Time</th>
                        <th>To</th>
                        <th>From</th>
                        <th>Duration</th>
                        <th>Direction</th>
                        <th>Context</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {info.map(rt =>
                        <tr key={rt.id}>
                            <td>{rt.startDateTimeFormatted}</td>
                            <td>{rt.to}</td>
                            <td>{rt.from}</td>
                            <td>{rt.durationFormatted}</td>
                            <td>{rt.direction}</td>
                            <td>{rt.context}</td>
                            <td>{rt.state}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Dashboard.renderDashboard(this.state.realtimedata);

        return (
            <div>
                <h1>Dashboard</h1>
                {contents}
            </div>
        );
    }
}