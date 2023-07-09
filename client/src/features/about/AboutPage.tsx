import React from 'react';
import { Typography, Box } from '@mui/material';
import { Transition } from 'react-transition-group';

const duration = 500; 
const defaultStyle = {
  transition: `transform ${duration}ms ease-in-out, opacity ${duration}ms ease-in-out`,
  transform: 'translateY(50px)',
  opacity: 0,
};

const transitionStyles: { [key: string]: any } = {
  entering: { transform: 'translateY(50px)', opacity: 0 },
  entered: { transform: 'translateY(0)', opacity: 1 },
};

const AboutPage: React.FC = () => {
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
              Dobrodošli u "Sve što treba"!
            </Typography>
            <Typography variant="body1">
              Mi smo vaša destinacija za sve što vam treba. Naša prodavnica
              nudi širok asortiman proizvoda za sve vaše potrebe.
            </Typography>
            <Typography variant="body1">
              Bilo da vam treba tehnika, kućni aparati, odeća, ili bilo šta
              drugo, mi smo tu da vam pružimo najbolju uslugu i kvalitetne
              proizvode.
            </Typography>
            <Typography variant="body1">
              Posetite našu prodavnicu danas i pronađite sve što vam treba!
            </Typography>
          </Box>
        )}
      </Transition>
    </Box>
  );
};

export default AboutPage;
