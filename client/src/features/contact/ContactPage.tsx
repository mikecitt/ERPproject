import React from "react";
import { Typography, Box } from "@mui/material";
import { Transition } from "react-transition-group";

const duration = 500; // Animation duration in milliseconds

const defaultStyle = {
  transition: `transform ${duration}ms ease-in-out, opacity ${duration}ms ease-in-out`,
  transform: "translateY(50px)",
  opacity: 0,
};

const transitionStyles: { [key: string]: any } = {
  entering: { transform: "translateY(50px)", opacity: 0 },
  entered: { transform: "translateY(0)", opacity: 1 },
};

const ContactPage: React.FC = () => {
  return (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      height="100vh"
    >
      <Transition in={true} timeout={duration} appear>
        {(state) => (
          <Box
            maxWidth={600}
            textAlign="center"
            style={{
              ...defaultStyle,
              ...transitionStyles[state],
            }}
          >
            <Typography variant="h4" align="center" gutterBottom>
              Kontaktirajte nas
            </Typography>
            <Typography variant="body1" gutterBottom>
              Ako imate pitanja ili vam je potrebna podrška, slobodno nas
              kontaktirajte:
            </Typography>
            <Typography variant="body1" gutterBottom>
              Email: info@climber.rs
            </Typography>
            <Typography variant="body1" gutterBottom>
              Telefon: +381 66 123 123
            </Typography>
            <Typography variant="body1" gutterBottom>
              Adresa: Balzakova 1, Novi Sad
            </Typography>
            <Typography variant="body1">
              Očekujemo vaše poruke i radujemo se saradnji!
            </Typography>
          </Box>
        )}
      </Transition>
    </Box>
  );
};

export default ContactPage;
