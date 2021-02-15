import { FILESTORE } from '../utils/axios-env-config'
import UrlBuilder from '../utils/url-builder'
import { getFile, deleteData} from './api'

export const downloadFile = (id, fileName) => {

    const url = new UrlBuilder({ host: FILESTORE.DOWNLOAD })
            .path(id).build()

    return getFile(url).then(response => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', fileName);
                document.body.appendChild(link);
                link.click();
        })
}

export const deleteFile = (id) => {

        const url = new UrlBuilder({ host: FILESTORE.PATH })
                .path(id).build()
    
        return deleteData(url).then(response => response.data)
    }




