class AddLogsToRecords < ActiveRecord::Migration
  def change
    add_column :records, :logs, :string
  end
end
