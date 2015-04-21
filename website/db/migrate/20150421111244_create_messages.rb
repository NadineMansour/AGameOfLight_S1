class CreateMessages < ActiveRecord::Migration
  def change
    create_table :messages do |t|
      t.belongs_to :teacher 
      t.belongs_to :student
      t.string :mess
      t.string :reply

      t.timestamps null: false
    end
    
  end
end
