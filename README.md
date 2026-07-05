# ego

ēgo is the system of record for persons in Mekniy and Lurk.

## Core philosophy

The platform is actor-centric.

Every significant operation in the platform is performed by or on behalf of a
`Person`. A `Person` is the canonical actor in the platform, independent of how
that actor authenticated. Authentication providers (currently Keycloak, and
potentially others in the future) are only mechanisms for establishing an
identity that maps to a `Person`.

This allows domain services (Citizen Registry, Audit, Messaging, Elections,
etc.) to reason in terms of `PersonId`, while the authentication layer remains
replaceable.

## Scope

The first version of the Person Registry is intentionally small and models only:

- Person ID
- Keycloak subject (`sub`)
- Preferred username
- Display name
- Email
- Status
- Created timestamp
- Updated timestamp

Non-goals include domain-specific data such as citizenship, organizations,
addresses, permissions, audit, and messaging.

## Identity synchronization

Identity synchronization is separate from registry storage so future identity
providers can be added without redesigning the registry.

Current synchronization flow:

1. User authenticates with Keycloak.
2. Read stable OIDC `sub`.
3. Lookup person by `sub`.
4. If found, update username/display name/email when changed.
5. If missing, create a person (JIT provisioning).
6. Return the person.

## API

The Person Registry is OpenAPI-first. See:

- `/home/runner/work/ego/ego/openapi/person-registry.yaml`

Initial endpoints:

- `GET /persons/{id}`
- `GET /persons/by-sub/{sub}`
- `POST /persons`
- `PATCH /persons/{id}`
- `GET /me`

`GET /me` resolves the authenticated JWT into the corresponding `Person`.

The service does not expose Keycloak administration APIs.
