class Logger {
    constructor() {
    }

    Information(message) {
        // eslint-disable-next-line
        console.log(message);
    }

    LogData(data) {
        // eslint-disable-next-line
        console.log(JSON.stringify(data))
    }
}

export default new Logger();