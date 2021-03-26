# CslaModelTemplates for Web APIs

CSLA model templates to quickly setup new models, data access and contracts
for .NET Core 3.1 applications. Provide data access templates for __MySQL__
and __SQL Server__ databases.

* [Simple Models](#simple-models)
* [Pagination](#pagination)
* [Selection](#selection)
* [Complex Models](#complex-models)
* [Tree Model](#tree-model)
* [Junction Models](#junction-models)

## Simple Models

![Simple model](/Simple.png "Simple model")

The simple models implement the use cases of a one-level entity.
Root entity: Team.

### SimpleList

The simple list template implements a read-only business collection.

Component | Description
--------- | -----------
SimpleTeamList | read-only root collection
SimpleTeamListItem | read-only child object

### SimpleView

The simple view template implements a read-only business object.

Component | Description
--------- | -----------
SimpleTeamView | read-only root object

### Simple

The simple template implements an editable business object.

Component | Description
--------- | -----------
SimpleTeam | read-only editable object

### SimpleSet

The simple set template implements an editable business collection.

Component | Description
--------- | -----------
SimpleTeamSet | editable root collection
SimpleTeamSetItem | editable child object

### SimpleCommand

The simple command template implements a command object.

Component | Description
--------- | -----------
RenameTeam | command object

## Pagination

The following templates provides variations of the simple list.

### SortedList

The sorted list template implements a read-only business collection
with sort options in the criteria.

Component | Description
--------- | -----------
SortedTeamList | read-only root collection
SortedTeamListItem | read-only child object

### PaginatedList

The paginated list template implements a read-only business collection
with pagination options in the criteria. The Data property of the root
object contains a page of the list, while the TotalCount property returns
the count of all items that match the criteria.

Component | Description
--------- | -----------
PaginatedTeamList | read-only root object
PaginatedTeamListItems | read-only child collection
PaginatedTeamListItem | read-only child object

### PaginatedSortedList

The paginated & sorted list template implements a read-only business
collection with pagination and sort options in the criteria. The Data
property of the root object contains a page of the list, while the
TotalCount property returns the count of all items that match the criteria.

Component | Description
--------- | -----------
PaginatedSortedTeamList | read-only root object
PaginatedSortedTeamListItems | read-only child collection
PaginatedSortedTeamListItem | read-only child object

## Selection

The selection templates provides more simplified versions of the simple list
that can be used e.g. in drop-down lists. The items of the selection list
have properties for a value and a description only.

### SelectionWithKey

The template implements the selection list with a value property named Key
whose data type is number.

Component | Description
--------- | -----------
TeamKeyChoice | read-only root collection
KeyNameOption | read-only child object

### SelectionWithCode

The template implements the selection list with a value property name Code
whose data type is string.

Component | Description
--------- | -----------
TeamCodeChoice | read-only root collection
CodeNameOption | read-only child object

## Complex Models

![Complex model](/Complex.png "Complex model")

The complex models implement the use cases of a multi-level entity.
Root entity: Team, child entity: Player.

### ComplexList

The complex list template implements a read-only business collection where
all collection elements have a read-only child collection.

Component | Description
--------- | -----------
TeamList | read-only root collection
TeamListItem | read-only child object
PlayerListItems | read-only child collection
PlayerListItem | read-only child object

### ComplexView

The complex view template implements a read-only business object with
a read-only child collection.

Component | Description
--------- | -----------
TeamView | read-only root object
PlayerViews | read-only child collection
PlayerView | read-only child object

### Complex

The complex template implements an editable business object with
an editable child collection.

Component | Description
--------- | -----------
Team | editable root object
Players | editable child collection
Player | editable child object

### ComplexSet

The complex set template implements an editable business collection
where all collection elements have an editable child collection.

Component | Description
--------- | -----------
TeamSet | editable root collection
TeamSetItem | editable child object
TeamSetPlayers | editable child collection
TeamSetPlayer | editable child object

### ComplexCommand

The complex command template implements a command object with a resulting
read-only child collection.

Component | Description
--------- | -----------
CountTeams | command object
CountTeamsList | read-only child collection
CountTeamsListItem | read-only child object

## Tree Model

![Tree model](/Tree.png "Tree model")

### Tree

The tree templates implements a special version of the complex list where
all child objects are the same as the root object.

Component | Description
--------- | -----------
FolderTree | read-only root object
FolderNodeList | read-only child collection
FolderNode | read-only child object

## Junction Models

![Junction model](/Junction.png "Junction model")

The junction models implement the use cases of two entities having a
junction or bridging entity. Root entities: Group and Person, junction
entity: GroupPersons.

### JunctionView

The junction view template implements a read-only business object with
a read-only member collection.

Component | Description
--------- | -----------
GroupView | read-only root object
MemberViews | read-only child collection
MemberView | read-only child object

### Junction

The junction template implements an editable business object with
an editable member collection.

Component | Description
--------- | -----------
Group | editable root object
Members | editable child collection
Member | editable child object

