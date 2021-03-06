import React from "react";
import { useHistory } from "react-router";
import { useMediaQuery } from "react-responsive";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import { NavLink } from "react-router-dom";

import { useUser } from "../../../../auth";
import { minWidth } from "../../../../utilities/media-queries";

import UserProfileButton from "./components/user-profile-button";
import "./top-bar.scss";

const TopBar = () => {
  const history = useHistory();
  const user = useUser();
  const isSmallOrLarger = useMediaQuery({ query: minWidth.SM });
  const isAuthenticated = !!user;

  return (
    <Navbar variant="dark" className="cnc-top-bar">
      <Navbar.Brand
        className="cnc-top-bar--brand"
        onClick={() => history.push("/")}
      >
        {isSmallOrLarger ? "Creatures & Caves" : "C&C"}
      </Navbar.Brand>
      <Nav className="ml-auto">
        {isAuthenticated ? (
          <UserProfileButton />
        ) : (
          <Nav.Link as={NavLink} className="cnc-top-bar--link" to="/login">
            Login
          </Nav.Link>
        )}
      </Nav>
    </Navbar>
  );
};

export default TopBar;
