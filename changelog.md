# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-database/releases/tag/1.0.0-preview.1) - 2021-02-17  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-database/milestone/1?closed=1)  
    

### Added

- Add add and remove methods for database ([#4](https://github.com/unity-game-framework/ugf-database/pull/4))  
    - Add `Add`, `AddAsync`, `Remove` and `RemoveAsync` methods for `IDatabase` and `IDatabase<TKey, TValue>`.
    - Add `Added` and `Removed` events for `IDatabase` and `IDatabase<TKey, TValue>`.
    - Add `DatabaseKeyHandler` and `DatabaseKeyHandler<TKey>` delegates for key only database events.
    - Add `DatabaseGetAsyncResult<TValue>` implicit conversion to `DatabaseGetAsyncResult` structure.
    - Add `DatabaseGetAsyncResult<TValue>` and `DatabaseGetAsyncResult` equality comparison implementation.
    - Add `DatabaseBase` and `Database<TKey, TValue>` default implementation for async methods.

### Removed

- Remove try set methods ([#5](https://github.com/unity-game-framework/ugf-database/pull/5))  
    - Remove `TrySet` and `TrySetAsync` methods from `IDatabase` and `IDatabase<TKey, TValue>`.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-database/releases/tag/1.0.0-preview) - 2021-02-15  

### Release Notes

- No release notes.

## [0.1.0-preview](https://github.com/unity-game-framework/ugf-database/releases/tag/0.1.0-preview) - 2021-02-13  

### Release Notes

- No release notes.


