declare const require: {
    context: (path: string, useSubdirectories: boolean, regExp: RegExp) => {
        (key: string): string;
        keys: () => string[];
    };
};
