import React from 'react';
import {
    ResponsiveContainer,
    LineChart,
    Line, 
    XAxis, 
    YAxis, 
    CartesianGrid, 
    Tooltip, 
    Legend,
} from 'recharts';

interface Props {
    data: Array<{
        name: String,
        uv: Number,
        pv: Number,
    }>
}

class SimpleLineChart extends React.PureComponent<Props> {
    render() {
        return (
            <ResponsiveContainer height={300} width='100%'>
                <LineChart
                    data={this.props.data}
                    margin={{top: 20, left: -10}}
                >
                    <XAxis dataKey="name" />
                    <YAxis />
                    <CartesianGrid strokeDasharray="3 3" />
                    <Tooltip />
                    <Legend />
                    <Line type="monotone" dataKey="pv" stroke="#5d80f9" activeDot={{r: 8}}/>
                    <Line type="monotone" dataKey="uv" stroke="#f3c363" />
                 </LineChart>
            </ResponsiveContainer>
        );
    }
}

export default SimpleLineChart;