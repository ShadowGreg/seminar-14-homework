import React, {ReactNode, useState } from 'react';
import './App.css';
import { Button, Paper, styled, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import axios from 'axios';

const Item = styled(Paper)(({ theme }) => ({
    ...theme.typography.body2,
    textAlign: 'center',
    color: theme.palette.text.secondary,
    height: 60,
    lineHeight: '60px',
}));

function App() {
    const [error, setError] = useState<string | null>(null);
    const [data, setData] = useState<any | null>(null);

    const get = () => {
        // axios.get(`http://localhost:5005/WeatherForecast`)
            axios<string>(`${process.env.REACT_APP_BACK_URL}/WeatherForecast`)

            .then(res => {
                setError(null);
                setData(res.data);
            })
            .catch(error => setError(JSON.stringify(error)));
    };

    return (
        <div className="App">
            <Button onClick={get} variant='contained'>Click GET</Button>
            {data && (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Property</TableCell>
                                <TableCell>Value</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {Object.entries(data).map(([key, value]) => (
                                <TableRow key={key}>
                                    <TableCell>{key}</TableCell>
                                    <TableCell>{value as ReactNode}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
            {error && (
                <Paper className="red p-10" elevation={2}>
                    {error}
                </Paper>
            )}
        </div>
    );
}

export default App;