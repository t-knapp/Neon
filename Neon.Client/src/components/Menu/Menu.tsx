import React, { ReactElement, ReactNode } from 'react';
import { Link, NavLink } from 'react-router-dom';

type Props = {
    children: ReactNode;
};

export default function Menu(props: Props): ReactElement {
    return (
        <div>
            <nav className='navbar navbar-expand-lg navbar-light bg-light'>
            <a className='navbar-brand' href='#'>Neon</a>
            <button className='navbar-toggler' type='button' data-toggle='collapse' data-target='#navbarSupportedContent' aria-controls='navbarSupportedContent' aria-expanded='false' aria-label='Toggle navigation'>
                <span className='navbar-toggler-icon' />
            </button>

            <div className='collapse navbar-collapse' id='navbarSupportedContent'>
                <ul className='navbar-nav mr-auto'>
                <li className='nav-item'>
                    <Link className='nav-link' to='/'>Gallerie</Link>
                </li>
                <li className='nav-item'>
                    <NavLink className='nav-link' to='/assets'>Liste</NavLink>
                </li>
                <li className='nav-item dropdown'>
                    <a className='nav-link dropdown-toggle' href='#' id='navbarDropdown' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>
                    Hinzufügen
                    </a>
                    <div className='dropdown-menu' aria-labelledby='navbarDropdown'>
                        <NavLink className='dropdown-item' to='/add/image'>Bild</NavLink>
                        <NavLink className='dropdown-item' to='/add/html'>Text</NavLink>
                    </div>
                </li>
                </ul>
            </div>
            </nav>
            {props.children}
        </div>
    );
}