CREATE TABLE logs
(
    timestamp TIMESTAMPTZ NOT NULL,
    level VARCHAR(20) NOT NULL,
    application VARCHAR(100),
    message TEXT,
    exception TEXT,
    properties JSONB,

    PRIMARY KEY (timestamp)
);