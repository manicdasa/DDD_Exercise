import React, { useState, useEffect } from 'react';
import DataTable from 'react-data-table-component';
import { Input, InputGroup, Button, Form } from 'reactstrap';
import { Container } from "reactstrap";
import _ from 'lodash';
import { BiSearchAlt2 } from 'react-icons/bi';
import ReactLoading from 'react-loading';
import { AiOutlineProject } from "react-icons/ai";
import { useAlert } from 'react-alert';

import { customStyles } from './AdminColumns';
import { GetNewDataFromPageChange, SortFunction } from '../../services/AdminServices';

export const DataTableComponent = ({columns, path, componentName, columnSort} : any) => 
{
    const [loadingState, setLoadingState] = useState(true);

    const [page, setPage] = useState(0);
    const [rows, setRows] = useState(10);

    

    const [searchParams, setSearchParams] = useState('');

    const [orderColumn, setOrderColumn] = useState(columnSort);

    const [sortDir, setSortDir] = useState("desc");


    const handleChangeParams = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setSearchParams(event.target.value);    
    }

    const [data, setData] = useState<{totalCount: number; items: never[];}>({ totalCount: 0, items: [] });
    const [filters, setFilters] = useState<{field: string; sortDirection: string;}>({field: '', sortDirection: ''})

    const alert = useAlert();

    const Sort = (rowsF: Array<any>[], field: string, sortDirection: "desc" | "asc") =>
    {
        
        if(field === null)
        {
           return rowsF;
        } 
        else
        {
            if (filters.field != field || filters.sortDirection != sortDirection) {
                setOrderColumn(field);
                setSortDir(sortDirection);
                SortFunction(path, 0, rows, searchParams, field, sortDirection, alert).then(response=> {setFilters({field, sortDirection}); setData(response);});
                return data.items;
          }
          else
            return rowsF;
        }
    }

    const changeRowPerPage = (perPage: number)=> 
    {
        setRows(perPage);
        GetNewDataFromPageChange(path, page, perPage, searchParams, orderColumn, sortDir, alert).then((response) => setData(response));
    }

    const changePage = (state: number) => 
    {
        setPage(state - 1);
        GetNewDataFromPageChange(path, state - 1, rows, searchParams, orderColumn, sortDir, alert).then((response) => setData(response));
    }

    useEffect(()=>
    {
        setLoadingState(true);
        SortFunction(path, page, rows, searchParams, columnSort, 'desc', alert).then(response=> { setData(response); setLoadingState(false); });
    },[])

    return(
            <div className="main-dashboard">
            <Container fluid >
                    <h4 className="dashboard-table-title dashProj">< AiOutlineProject /> &nbsp; {componentName}</h4>
            <div className="row-style search">
                    <Form className="row-style" onSubmit={(e) => { e.preventDefault(); setLoadingState(true); GetNewDataFromPageChange(path, 0, rows, searchParams, orderColumn, sortDir, alert).then((response) => { setData(response); setLoadingState(false); })}}>
            <InputGroup>
              <Input type="text" className="input-search-proj" placeholder="Search projects" value={searchParams} onChange={handleChangeParams} />
            </InputGroup>
            <Button color="primary" type="submit"><BiSearchAlt2 /></Button>
            </Form>
            </div>
            <br/>
            { loadingState ? <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                <ReactLoading className="mt-5" type='bars' color='#0080FF' />
            </div> :
            <DataTable
                keyField={'id'}
                data={data.items}
                columns={columns}
                noHeader
                striped
                highlightOnHover
                sortFunction={(rows: Array<any>[], field: string, sortDirection: "desc" | "asc") => { return Sort(rows, field, sortDirection) as Array<any>[]; }}
                pointerOnHover
                pagination 
                paginationServer
                paginationResetDefaultPage
                paginationRowsPerPageOptions={[5,10,15,20,25,50,100]}
                paginationTotalRows={data.totalCount}
                onChangePage={changePage}
                onChangeRowsPerPage={changeRowPerPage}
                customStyles={customStyles}
                responsive={true}
                className="table-width dashboard"/> }
                </Container>
                </div>
        )
}

export default DataTableComponent;