using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Entities;
using MongoDB.Entities.Core;

namespace Neon.Server {

    public class MongoCollection<T> : IMongoCollection<T> where T : Entity {

        private readonly IMongoCollection<T> _collection;


        public MongoCollection( DB database )
            => _collection = database?.Collection<T>() ?? throw new ArgumentNullException( nameof( database ) );


        public CollectionNamespace CollectionNamespace => _collection.CollectionNamespace;
        public IMongoDatabase Database => _collection.Database;
        public IBsonSerializer<T> DocumentSerializer => _collection.DocumentSerializer;
        public IMongoIndexManager<T> Indexes => _collection.Indexes;
        public MongoCollectionSettings Settings => _collection.Settings;


        public IAsyncCursor<TResult> Aggregate<TResult>( PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.Aggregate( pipeline, options, cancellationToken );
        public IAsyncCursor<TResult> Aggregate<TResult>( IClientSessionHandle session, PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default ) 
            => _collection.Aggregate( session, pipeline, options, cancellationToken );
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>( PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.AggregateAsync( pipeline, options, cancellationToken );
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>( IClientSessionHandle session, PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default ) 
            => _collection.AggregateAsync( session, pipeline, options, cancellationToken );


        public BulkWriteResult<T> BulkWrite( IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.BulkWrite( requests, options, cancellationToken );
        public BulkWriteResult<T> BulkWrite( IClientSessionHandle session, IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default )
             => _collection.BulkWrite( session, requests, options, cancellationToken );
        public Task<BulkWriteResult<T>> BulkWriteAsync( IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.BulkWriteAsync( requests, options, cancellationToken );
        public Task<BulkWriteResult<T>> BulkWriteAsync( IClientSessionHandle session, IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.BulkWriteAsync( session, requests, options, cancellationToken );

        public long Count( FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocuments( filter, options, cancellationToken );
        public long Count( IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocuments( session, filter, options, cancellationToken );
        public Task<long> CountAsync( FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocumentsAsync( filter, options, cancellationToken );
        public Task<long> CountAsync( IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocumentsAsync( session, filter, options, cancellationToken );

        public long CountDocuments( FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocuments(  filter, options, cancellationToken );
        public long CountDocuments( IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocuments( session, filter, options, cancellationToken );
        public Task<long> CountDocumentsAsync( FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
             => _collection.CountDocumentsAsync( filter, options, cancellationToken );
        public Task<long> CountDocumentsAsync( IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.CountDocumentsAsync( session, filter, options, cancellationToken );

        public DeleteResult DeleteMany( FilterDefinition<T> filter, CancellationToken cancellationToken = default )
            => _collection.DeleteMany( filter, cancellationToken );
        public DeleteResult DeleteMany( FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default )
            => _collection.DeleteMany( filter, options, cancellationToken );
        public DeleteResult DeleteMany( IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.DeleteMany( session, filter, options, cancellationToken );

        public Task<DeleteResult> DeleteManyAsync( FilterDefinition<T> filter, CancellationToken cancellationToken = default ) 
            => _collection.DeleteManyAsync( filter, cancellationToken );
        public Task<DeleteResult> DeleteManyAsync( FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default )
            => _collection.DeleteManyAsync( filter, options, cancellationToken );
        public Task<DeleteResult> DeleteManyAsync( IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.DeleteManyAsync( session, filter, options, cancellationToken );

        public DeleteResult DeleteOne( FilterDefinition<T> filter, CancellationToken cancellationToken = default )
            => _collection.DeleteOne( filter, cancellationToken );
        public DeleteResult DeleteOne( FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default )
            => _collection.DeleteOne( filter, options, cancellationToken );
        public DeleteResult DeleteOne( IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.DeleteOne( session, filter, options, cancellationToken );

        public Task<DeleteResult> DeleteOneAsync( FilterDefinition<T> filter, CancellationToken cancellationToken = default )
            => _collection.DeleteOneAsync( filter, cancellationToken );
        public Task<DeleteResult> DeleteOneAsync( FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default )
            => _collection.DeleteOneAsync( filter, options, cancellationToken );
        public Task<DeleteResult> DeleteOneAsync( IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default )
            => _collection.DeleteOneAsync( session, filter, options, cancellationToken );

        public IAsyncCursor<TField> Distinct<TField>( FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default )
            => _collection.Distinct( field, filter, options, cancellationToken );
        public IAsyncCursor<TField> Distinct<TField>( IClientSessionHandle session, FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default )
             => _collection.Distinct( session, field, filter, options, cancellationToken );

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>( FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default )
            => _collection.DistinctAsync( field, filter, options, cancellationToken );
        public Task<IAsyncCursor<TField>> DistinctAsync<TField>( IClientSessionHandle session, FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default )
            => _collection.DistinctAsync( session, field, filter, options, cancellationToken );

        public long EstimatedDocumentCount( EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.EstimatedDocumentCount( options, cancellationToken );
        public Task<long> EstimatedDocumentCountAsync( EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default )
            => _collection.EstimatedDocumentCountAsync( options, cancellationToken );

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>( FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindAsync( filter, options, cancellationToken );
        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
             => _collection.FindAsync( session, filter, options, cancellationToken );

        public TProjection FindOneAndDelete<TProjection>( FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndDelete( filter, options, cancellationToken );
        public TProjection FindOneAndDelete<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndDelete( session, filter, options, cancellationToken );

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>( FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndDeleteAsync( filter, options, cancellationToken );
        public Task<TProjection> FindOneAndDeleteAsync<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndDeleteAsync( session, filter, options, cancellationToken );

        public TProjection FindOneAndReplace<TProjection>( FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndReplace( filter, replacement, options, cancellationToken );
        public TProjection FindOneAndReplace<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndReplace( session, filter, replacement, options, cancellationToken );

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>( FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndReplaceAsync( filter, replacement, options, cancellationToken );
        public Task<TProjection> FindOneAndReplaceAsync<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndReplaceAsync( session, filter, replacement, options, cancellationToken );

        public TProjection FindOneAndUpdate<TProjection>( FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default ) 
            => _collection.FindOneAndUpdate( filter, update, options, cancellationToken );
        public TProjection FindOneAndUpdate<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndUpdate( session, filter, update, options, cancellationToken );

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>( FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndUpdateAsync( filter, update, options, cancellationToken );
        public Task<TProjection> FindOneAndUpdateAsync<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindOneAndUpdateAsync( session, filter, update, options, cancellationToken );

        public IAsyncCursor<TProjection> FindSync<TProjection>( FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindSync( filter, options, cancellationToken );
        public IAsyncCursor<TProjection> FindSync<TProjection>( IClientSessionHandle session, FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default )
            => _collection.FindSync( session, filter, options, cancellationToken );

        public void InsertMany( IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertMany( documents, options, cancellationToken );
        public void InsertMany( IClientSessionHandle session, IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertMany( session, documents, options, cancellationToken );

        public Task InsertManyAsync( IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertManyAsync( documents, options, cancellationToken );
        public Task InsertManyAsync( IClientSessionHandle session, IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertManyAsync( session, documents, options, cancellationToken );

        public void InsertOne( T document, InsertOneOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertOne( document, options, cancellationToken );
        public void InsertOne( IClientSessionHandle session, T document, InsertOneOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertOne( session, document, options, cancellationToken );

        public Task InsertOneAsync( T document, CancellationToken cancellationToken )
            => _collection.InsertOneAsync( document, cancellationToken );
        public Task InsertOneAsync( T document, InsertOneOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertOneAsync( document, options, cancellationToken );
        public Task InsertOneAsync( IClientSessionHandle session, T document, InsertOneOptions options = null, CancellationToken cancellationToken = default )
            => _collection.InsertOneAsync( session, document, options, cancellationToken );

        public IAsyncCursor<TResult> MapReduce<TResult>( BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default )
            => _collection.MapReduce( map, reduce, options, cancellationToken );
        public IAsyncCursor<TResult> MapReduce<TResult>( IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default )
            => _collection.MapReduce( session, map, reduce, options, cancellationToken );

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>( BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default )
            => _collection.MapReduceAsync( map, reduce, options, cancellationToken );
        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>( IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default )
            => _collection.MapReduceAsync( session, map, reduce, options, cancellationToken );

        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : T
            => _collection.OfType<TDerivedDocument>();

        public ReplaceOneResult ReplaceOne( FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default )
            => _collection.ReplaceOne( filter, replacement, options, cancellationToken );
        public ReplaceOneResult ReplaceOne( FilterDefinition<T> filter, T replacement, UpdateOptions options, CancellationToken cancellationToken = default )
             => _collection.ReplaceOne( filter, replacement, options, cancellationToken );
        public ReplaceOneResult ReplaceOne( IClientSessionHandle session, FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default )
            => _collection.ReplaceOne( session, filter, replacement, options, cancellationToken );
        public ReplaceOneResult ReplaceOne( IClientSessionHandle session, FilterDefinition<T> filter, T replacement, UpdateOptions options, CancellationToken cancellationToken = default )
            => _collection.ReplaceOne( session, filter, replacement, options, cancellationToken );

        public Task<ReplaceOneResult> ReplaceOneAsync( FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default )
            => _collection.ReplaceOneAsync( filter, replacement, options, cancellationToken );
        public Task<ReplaceOneResult> ReplaceOneAsync( FilterDefinition<T> filter, T replacement, UpdateOptions options, CancellationToken cancellationToken = default )
            => _collection.ReplaceOneAsync( filter, replacement, options, cancellationToken );
        public Task<ReplaceOneResult> ReplaceOneAsync( IClientSessionHandle session, FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default )
            => _collection.ReplaceOneAsync( session, filter, replacement, options, cancellationToken );
        public Task<ReplaceOneResult> ReplaceOneAsync( IClientSessionHandle session, FilterDefinition<T> filter, T replacement, UpdateOptions options, CancellationToken cancellationToken = default )
            => _collection.ReplaceOneAsync( session, filter, replacement, options, cancellationToken );

        public UpdateResult UpdateMany( FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateMany( filter, update, options, cancellationToken );
        public UpdateResult UpdateMany( IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateMany( session, filter, update, options, cancellationToken );

        public Task<UpdateResult> UpdateManyAsync( FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateManyAsync( filter, update, options, cancellationToken );
        public Task<UpdateResult> UpdateManyAsync( IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateManyAsync( session, filter, update, options, cancellationToken );

        public UpdateResult UpdateOne( FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateOne( filter, update, options, cancellationToken );
        public UpdateResult UpdateOne( IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateOne( session, filter, update, options, cancellationToken );

        public Task<UpdateResult> UpdateOneAsync( FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateOneAsync( filter, update, options, cancellationToken );
        public Task<UpdateResult> UpdateOneAsync( IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default )
            => _collection.UpdateOneAsync( session, filter, update, options, cancellationToken );

        public IChangeStreamCursor<TResult> Watch<TResult>( PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default )
            => _collection.Watch( pipeline, options, cancellationToken );
        public IChangeStreamCursor<TResult> Watch<TResult>( IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default )
            => _collection.Watch( session, pipeline, options, cancellationToken );

        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>( PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default )
            => _collection.WatchAsync( pipeline, options, cancellationToken );
        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>( IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default )
            => _collection.WatchAsync( session, pipeline, options, cancellationToken );

        public IMongoCollection<T> WithReadConcern( ReadConcern readConcern )
            => _collection.WithReadConcern( readConcern );
        public IMongoCollection<T> WithReadPreference( ReadPreference readPreference )
            => _collection.WithReadPreference( readPreference );
        public IMongoCollection<T> WithWriteConcern( WriteConcern writeConcern )
            => _collection.WithWriteConcern( writeConcern );
    }
}
