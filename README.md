# PatternsInt-SAP_ESP

Simplified copy trading platform backend for the SAP / Enterprise Systems Patterns task.

## Project goal

This repository contains a microservice-based backend for a simplified copy trading platform.

The system includes:

- Trader Service
- Subscription Service
- Accounting Service
- Exchange Proxy Service
- Copy Engine Service

## Planned architecture

Services communicate asynchronously through an event bus.

Planned supporting infrastructure:

- RabbitMQ
- PostgreSQL
- Jaeger
- Docker Compose

## Branch strategy

- main - final stable version
- develop - integration branch
- eature/dayX-* - feature branches for daily progress

## Day 1 scope

- Create solution structure
- Create service skeletons
- Add shared projects
- Add Docker Compose foundation
- Add documentation folders
- Prepare repository for implementation

## Planned deliverables

- Service data schemas
- Happy path and failure path dynamic diagrams
- Pattern analysis table
- Load testing scripts
- Final performance summary

## How to run infrastructure only

`powershell
docker compose up -d
