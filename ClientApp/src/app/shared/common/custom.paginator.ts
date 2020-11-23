import { MatPaginatorIntl } from '@angular/material/paginator';

export function CustomPaginator() 
{
    const customPaginatorIntl = new MatPaginatorIntl(); 
    customPaginatorIntl.itemsPerPageLabel = 'Itens por página';

    customPaginatorIntl.getRangeLabel = (page: number, pageSize: number, length: number) => {
        if (length === 0 || pageSize === 0) {
            return `0 of ${length}`;
        }
        length = Math.max(length, 0); const startIndex = page * pageSize;
        const endIndex = startIndex < length ? Math.min(startIndex + pageSize, length) : startIndex + pageSize;
        const label = `${startIndex + 1} - ${endIndex} de ${length}`
        return label;
    }

    return customPaginatorIntl;
}