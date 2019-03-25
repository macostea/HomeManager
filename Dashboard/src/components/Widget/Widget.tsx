import React, { ReactNode, ReactNodeArray } from 'react';
import PropTypes from 'prop-types';

import './Widget.scss';

interface Props {
    title: ReactNode,
    className: String,
    children: React.ReactNode,
}

class Widget extends React.Component<Props> {
    static defaultProps = {
        title: null,
        className: '',
        children: [],
    };

    render() {
        return (
            <section className={["widget", this.props.className].join(" ")}>
            {this.props.title &&
            (typeof this.props.title == 'string' ? (
                <h5 className="title">{this.props.title}</h5>
            ) : (
                <header className="title">{this.props.title}</header>
            ))}
            <div>{this.props.children}</div>
            </section>
        );
    }
}

export default Widget;
