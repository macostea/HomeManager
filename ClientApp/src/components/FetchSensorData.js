import React, { Component } from 'react';

export class FetchSensorData extends Component {
    displayName = FetchSensorData.name

    constructor(props) {
        super(props);
        this.state = { readings: [], loading: true };

        fetch('api/TemperatureSensor/SensorReadings')
            .then(response => response.json())
            .then(data => {
                this.setState({ readings: data, loading: false });
            });
    }

    static renderReadingsTable(readings) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                    </tr>
                </thead>
                <tbody>
                    {readings.map(reading =>
                        <tr key={reading.time}>
                            <td>{reading.time}</td>
                            <td>{reading.reading}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchSensorData.renderReadingsTable(this.state.readings);

        return (
            <div>
                <h1>Sensor readings</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }
}
