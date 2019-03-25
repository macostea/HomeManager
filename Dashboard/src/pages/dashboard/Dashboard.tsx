import React from 'react';
import { Row, Col } from 'reactstrap';
import Widget from '../../components/Widget/Widget';
import SimpleLineChart from '../../components/Charts/LineChart';

const data = [
    {name: 'Page A', uv: 4000, pv: 2400, amt: 2400},
    {name: 'Page B', uv: 3000, pv: 1398, amt: 2210},
    {name: 'Page C', uv: 2000, pv: 9800, amt: 2290},
    {name: 'Page D', uv: 2780, pv: 3908, amt: 2000},
    {name: 'Page E', uv: 1890, pv: 4800, amt: 2181},
    {name: 'Page F', uv: 2390, pv: 3800, amt: 2500},
    {name: 'Page G', uv: 3490, pv: 4300, amt: 2100},
  ];

class Dasboard extends React.Component<any, any> {
    render() {
        return (
            <div>
                <h1 className="page-title mb-lg">Pretty <span className="fw-semi-bold">Charts</span></h1>
                <Row>
                    <Col xs={12} md={6}>
                        <Widget
                            title={<h5>Simple <span className="fw-semi-bold">Line Chart</span></h5>}>
                            <SimpleLineChart data={data}></SimpleLineChart>
                        </Widget>
                    </Col>
                </Row>
            </div>
        )
    }
}

export default Dasboard;