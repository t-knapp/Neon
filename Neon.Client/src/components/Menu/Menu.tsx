import React, { ReactElement, ReactNode } from 'react';
import { Link, NavLink } from 'react-router-dom';

type Props = {
    children: ReactNode;
};

export default function Menu(props: Props): ReactElement {
    return (
        <div>
            <div>
                <ul className='nav nav-pills'>
                    <li className='nav-item'>
                        <Link className='nav-link' to='/'>Gallerie</Link>
                    </li>
                    <li className='nav-item'>
                        <NavLink className='nav-link' to='/assets'>Bilder</NavLink>
                    </li>
                    <li className='nav-item'>
                        <NavLink className='nav-link' to='/add'>Hinzufügen</NavLink>
                    </li>
                </ul>
            </div>
            {props.children}
        </div>
    );
}