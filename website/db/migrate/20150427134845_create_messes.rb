class CreateMesses < ActiveRecord::Migration
  def change
    create_table :messes do |t|
      t.string :semail
      t.string :remail
      t.string :text

      t.timestamps null: false
    end
  end
end
