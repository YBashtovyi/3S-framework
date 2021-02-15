export class UrlString {
    private _queryString = '';
    public get value() {
        return this._queryString;
    }
    public addParameter(name: string, value: any): void {
        if (value !== undefined && value !== null) {
            let prefix = this._queryString ? '&' : '';
            this._queryString = this._queryString + `${prefix}${name}=${value}`;
        }
    }
}


export class StringUtils {
    /**
  * Get strings diff and start position of diff.
  * @param { string } a - string to cut from b;
  * @param { string } b - subsequence containing all of the letters in "a" in the same order;
  * @return { ICompareStringResult } 
  */
    public compareStrings(a: string, b: string): ICompareStringResult {
        let i = 0;
        let j = 0;
        let result = <ICompareStringResult>{
            startPos: 0,
            diff: ""
        };

        while (j < b.length) {
            if (a[i] != b[j] || i == a.length) {
                result.diff += b[j];
                if (!result.startPos)
                    result.startPos = j;
            }
            else
                i++;
            j++;
        }
        if (result.diff.match(/^\s/g)) {
            result.diff = result.diff.substr(1);
            result.startPos += 1;
        }
        return result;
    }
} 

interface ICompareStringResult {
    startPos: number;
    diff: string;
}