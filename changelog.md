# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0-preview](https://github.com/unity-game-framework/ugf-database/releases/tag/1.1.0-preview) - 2021-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-database/milestone/5?closed=1)  
    

### Added

- Add Clear and ClearAsync methods ([#13](https://github.com/unity-game-framework/ugf-database/pull/13))  
    - Add `Cleared` event for `IDatabase` and `IDatabase<TKey, TValue>` interfaces and related implementations.
    - Add `Clear` and `ClearAsync` methods for `IDatabase` interface and related implementations.
    - Add implementation of `Clear` method for `PrefsDatabase` to clear all player prefs.
    - Add implementation of `Clear` method for `MemoryDatabase` to clear all values.

## [1.0.0-preview.4](https://github.com/unity-game-framework/ugf-database/releases/tag/1.0.0-preview.4) - 2021-07-13  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-database/milestone/4?closed=1)  
    

### Fixed

- Fix MemoryDatabase missing read only collection initialization ([#12](https://github.com/unity-game-framework/ugf-database/pull/12))  
    - Fix `MemoryDatabase` values read only collection initialization from constructors.

## [1.0.0-preview.3](https://github.com/unity-game-framework/ugf-database/releases/tag/1.0.0-preview.3) - 2021-07-13  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-database/milestone/3?closed=1)  
    

### Added

- Add MemoryDatabase constructors to pass dictionary ([#9](https://github.com/unity-game-framework/ugf-database/pull/9))  
    - Add constructor with initial _capacity_ and _comparer_.
    - Add constructor with `Dictionary<string, object>` as values and _comparer_.
    - Add constructor with `IDictionary<string, object>` as values and _comparer_.

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-database/releases/tag/1.0.0-preview.2) - 2021-02-27  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-database/milestone/2?closed=1)  
    

### Added

- Add try get and get with generic type argument ([#7](https://github.com/unity-game-framework/ugf-database/pull/7))  
    - Add `DatabaseExtensions` class with `Get<T>`, `TryGet<T>`, `GetAsync<T>` and `TryGetAsync<T>` extension methods for `IDatabase`.

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


